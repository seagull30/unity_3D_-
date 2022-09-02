using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Baldi : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        trace
    }
    private Animation Animation;
    private NavMeshAgent _agent;

    [SerializeField]
    private float Speed;

    private float _coolTime = 3.0f;
    private float _reduceCoolDown = 1.2f;

    private GameObject Target;
    private Vector3 TargetPos = new Vector3();

    private AudioSource _source;
    public AudioClip MoveSound;

    private State state;

    private Collider[] _targetCandidates = new Collider[5];
    private int _targetCandidateCount;
    private int _layerMask;
    private float _detectionRange = 100f;


    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _agent = GetComponent<NavMeshAgent>();
        Animation = GetComponent<Animation>();
        _source = GetComponent<AudioSource>();
        _agent.updateRotation = false;
        TargetPos = gameObject.transform.position;
        _layerMask = 1 << (LayerMask.NameToLayer("Player"));

    }

    private void OnEnable()
    {
        // 상속후 속도 각각 설정.
        GameManager.Instance.BookEvent += PlayerFoundBook;
        GameManager.Instance.Playersound += FindTarget;

        Speed = 15f;
        _agent.speed = Speed;
        StartCoroutine(movedelay());
    }

    IEnumerator movedelay()
    {
        while (true)
        {
            if (GameManager.Instance.IsGameOver)
                yield break;

            yield return new WaitForSeconds(_coolTime);
            _source.PlayOneShot(MoveSound);
            _agent.speed = Speed;
            yield return new WaitForSeconds(2.0f);
            _agent.speed = 0f;
            //A.velocity = new Vector3(0f,0f,0f);
        }
    }


    private void OnDisable()
    {
        GameManager.Instance.BookEvent -= PlayerFoundBook;
        GameManager.Instance.Playersound -= FindTarget;
    }

    void Start()
    {
        ChangeState(State.Idle);
    }

    //float extraRotationSpeed = 5f;
    private void Update()
    {
        Vector2 forward = new Vector2(transform.position.z, transform.position.x);
        Vector2 steeringTarget = new Vector2(_agent.steeringTarget.z, _agent.steeringTarget.x);

        //방향을 구한 뒤, 역함수로 각을 구한다.
        Vector2 dir = steeringTarget - forward;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Debug.Log(angle);

        //방향 적용
        //if (angle != 0)
        //{
        //    _agent.velocity = Vector3.zero;
            transform.eulerAngles = Vector3.up * angle;
        //}

        //Vector3 lookrotation = _agent.steeringTarget - transform.position;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookrotation), extraRotationSpeed * Time.deltaTime);

        //switch (state)
        //{
        //    case State.Idle: UpdateIdle(); break;
        //    case State.Patrol: UpdatePatrol(); break;
        //    case State.trace: UpdateTrace(); break;
        //}
    }

    void ChangeState(State nextState)
    {
        //StopAllCoroutines();

        state = nextState;

        switch (state)
        {
            case State.Idle: StartCoroutine(CoroutineIdle()); break;
            case State.Patrol: StartCoroutine(CoroutinePatrol()); break;
            case State.trace: StartCoroutine(CoroutineTrace()); break;
        }
    }

    //public void UpdateIdle()
    //{
    //
    //}
    //
    //public void UpdatePatrol()
    //{
    //
    //}
    //
    //public void UpdateTrace()
    //{
    //
    //}

    private IEnumerator CoroutineIdle()
    {
        TargetPos = transform.position;
        //yield return new WaitForSeconds(10f);
        ChangeState(State.Patrol);
        yield break;

    }
    private IEnumerator CoroutinePatrol()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, TargetPos) < 1f)
            {
                //랜덤 좌표 찍고 TargetPos 갱신
                RandomPoint(out Vector3 asd);
                TargetPos = asd;
                Debug.Log(TargetPos);
                _agent.SetDestination(TargetPos);
            }
            if (SearchTarget())
            {
                ChangeState(State.trace);
                yield break;
            }

            yield return null;
        }

    }
    private IEnumerator CoroutineTrace()
    {
        while (true)
        {
            if (Target != null)
            {
                TargetPos = Target.transform.position;
            }
            else
            {
                if (Vector3.Distance(TargetPos, transform.position) < 1f)
                {
                    ChangeState(State.Patrol);
                    yield break;
                }
            }
            _agent.SetDestination(TargetPos);
            if (Vector3.Distance(transform.position, TargetPos) > _detectionRange)
            {
                Target = null;
                SearchTarget();
            }


            //움직이기
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            CrashEvent();
        }
    }

    private void CrashEvent()
    {
        Debug.Log("님죽음");
        GameManager.Instance.GameOver();
    }

    private void PlayerFoundBook()
    {
        _coolTime /= _reduceCoolDown;
    }

    private void FindTarget(GameObject player)
    {
        Target = player;
        TargetPos = Target.transform.position;
        Debug.Log(TargetPos);
        ChangeState(State.trace);
    }

    void RandomPoint(out Vector3 result)
    {
        Debug.Log("위치 찾는중");
        for (int i = 0; i < 30; ++i)
        {
            Vector3 dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            Vector3 randomPoint = transform.position + dir * Random.Range(30f, 50f);
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                Debug.Log(hit.position);

                return;
            }
        }
        result = transform.position;
    }


    private bool SearchTarget()
    {
        Debug.Log("찾는중");
        _targetCandidates = new Collider[5];
        _targetCandidateCount = Physics.OverlapSphereNonAlloc(transform.position, 80f, _targetCandidates, _layerMask);
        foreach (Collider targetCandidate in _targetCandidates)
        {
            if (targetCandidate == null)
                continue;
            GameObject target = targetCandidate.gameObject;
            Debug.Assert(target != null);
            Debug.Log("찾음");
            Target = target;
            return true;
        }
        return false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, _detectionRange);
    //}

}
