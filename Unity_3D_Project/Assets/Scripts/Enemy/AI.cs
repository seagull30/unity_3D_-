using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public enum State
    {
        Idle,
        Patrol,
        trace
    }
    private Animation Animation;
    private NavMeshAgent A;

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
    [SerializeField]
    private float _detectionRange = 30f;


    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        A = GetComponent<NavMeshAgent>();
        Animation = GetComponent<Animation>();
        _source = GetComponent<AudioSource>();
        
        TargetPos = gameObject.transform.position;
        _layerMask = 1 << (LayerMask.NameToLayer("Player"));

    }

    private void OnEnable()
    {
        // 상속후 속도 각각 설정.
        GameManager.Instance.FindBook += PlayerFoundBook;
        GameManager.Instance.Playersound += FindTarget;
        Speed = 5f;
        A.speed = Speed;
        StartCoroutine(movedelay());
    }

    IEnumerator movedelay()
    {
        while(true)
        {
            if(GameManager.Instance.IsGameOver)
                yield break;

            _source.PlayOneShot(MoveSound);
            A.isStopped = false;
            yield return new WaitForSeconds(_coolTime);
            A.isStopped=true;
            //A.velocity = new Vector3(0f,0f,0f);
            yield return new WaitForSeconds(_coolTime);

        }
    }


    private void OnDisable()
    {
        GameManager.Instance.FindBook -= PlayerFoundBook;
        GameManager.Instance.Playersound -= FindTarget;
    }

    void Start()
    {
        ChangeState(State.Idle);
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle: UpdateIdle(); break;
            case State.Patrol: UpdatePatrol(); break;
            case State.trace: UpdateTrace(); break;
        }
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



    public void UpdateIdle()
    {

    }

    public void UpdatePatrol()
    {

    }

    public void UpdateTrace()
    {

    }

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
                A.SetDestination(TargetPos);
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
            A.SetDestination(TargetPos);
            if (Vector3.Distance(transform.position, TargetPos) > _detectionRange)
            {
                Target = null;
                SearchTarget();
            }


            //움직이기
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CrashEvent();
        }
    }

    private void CrashEvent()
    {
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
        ChangeState(State.trace);
    }

    void RandomPoint(out Vector3 result)
    {
        Debug.Log("위치 찾는중");
        for (int i = 0; i < 30; ++i)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * _detectionRange;
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



}
