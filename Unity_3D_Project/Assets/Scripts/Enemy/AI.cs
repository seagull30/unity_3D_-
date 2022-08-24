using System.Collections;
using System.Collections.Generic;
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

    private float InitSpeed;
    private float Speed;

    private float _coolTime = 3f;
    private float _reduceCoolDown = 1.2f;

    private GameObject Target;
    private Vector3 TargetPos;
    private AudioSource _source;
    private State state;
    private bool _findTarget = false;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        A = GetComponent<NavMeshAgent>();
        Animation = GetComponent<Animation>();
        TargetPos = gameObject.transform.position;
    }

    private void OnEnable()
    {
        // 상속후 속도 각각 설정.
        InitSpeed = 1f;
        Speed = InitSpeed;
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
        StopAllCoroutines();

        state = nextState;

        //_animator.SetBool(AnimID.ISIDLE, false);
        //_animator.SetBool(AnimID.ISWALK, false);
        //_animator.SetBool(AnimID.ISRUN, false);

        switch (state)
        {
            case State.Idle: StartCoroutine(CoroutineIdle()); break;
            case State.Patrol: StartCoroutine(CoroutinePatrol()); break;
            case State.trace: StartCoroutine(CoroutineTrace()); break;
        }
    }
    //class asd
    //{
    //    private NavMeshAgent _nav;
    //
    //    private Transform _trnasforrm;
    //    private float _range = 10;
    //    public Transform targetPos;
    //    Vector3 _point;
    //
    //    private void Start()
    //    {
    //    }
    //
    //    void Update()
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            if (RandomPoint(out _point))
    //            {
    //                targetPos.position = _point;
    //            }
    //        }
    //        _nav.SetDestination(targetPos.position);
    //    }
    //
    //    bool RandomPoint(out Vector3 result)
    //    {
    //        for (int i = 0; i < 30; ++i)
    //        {
    //            Vector3 randomPoint = _trnasforrm.position + Random.insideUnitSphere * _range;
    //            NavMeshHit hit;
    //            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
    //            {
    //                result = hit.position;
    //                return true;
    //            }
    //        }
    //        result = Vector3.zero;
    //        return false;
    //    }
    //}


    //public void Warp(float dist)
    //{
    //    int layer = 1 << NavMesh.GetAreaFromName("Walkable");
    //    Vector3 pos = RandomNavSphere(transform.position, dist, layer);
    //    transform.position = pos;
    //}
    //
    //public Vector3 RandomNavSphere(Vector3 origin, float dist, int mask)
    //{
    //    Vector3 randDirection = Random.insideUnitSphere * dist;
    //    randDirection += origin;
    //
    //    NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, mask);
    //    return navHit.position;
    //}

    public void UpdateIdle()
    {

    }

    public void UpdatePatrol()
    {

    }

    public void UpdateTrace()
    {
        TargetPos = Target.transform.position;
    }

    public virtual IEnumerator CoroutineIdle()
    {
        TargetPos = new Vector3(0, 0, 0);
        ChangeState(State.Patrol);
        yield break;

    }
    public virtual IEnumerator CoroutinePatrol()
    {
        while (true)
        {

            if (_findTarget)
            {
                ChangeState(State.trace);
                yield break;
            }

            if (transform.position == TargetPos)
            {
                //랜덤 좌표 찍고 TargetPos 갱신
                A.SetDestination(TargetPos);
            }

            yield return null;
        }

    }
    public virtual IEnumerator CoroutineTrace()
    {
        while (true)
        {

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CrashEvent();
        }
    }

    
    private void OnEnable()
    {

    }

    private void CrashEvent()
    {
        GameManager.Instance.GameOver();
    }



    private void playerFoundBook()
    {
        _coolTime /= _reduceCoolDown;
    }







    //bool IsFindEnemy()
    //{
    //
    //    if (!target.activeSelf) return false;
    //    Bounds targetBounds = target.GetComponentInChildren<SkinnedMeshRenderer>().bounds;
    //    _eyePlanes = GeometryUtility.CalculateFrustumPlanes(_eye);
    //    _isFindEnemy = GeometryUtility.TestPlanesAABB(_eyePlanes, targetBounds);
    //
    //    return _isFindEnemy;
    //}




    //void UpdateWalk()
    //{
    //    if (IsFindEnemy())
    //    {
    //        ChangeState(State.Run);
    //        return;
    //    }
    //    // 목적지까지 이동하는 코스
    //    Vector3 dir = _targetPosition - transform.position;
    //    if (dir.sqrMagnitude <= 0.2f)
    //    {
    //        ChangeState(State.Idle);
    //        return;
    //    }
    //
    //    var targetRotation = Quaternion.LookRotation(_targetPosition - transform.position, Vector3.up);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    //
    //    transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    //
    //}
    //
    //void UpdateRun()
    //{
    //    // 목적지까지 이동하는 코스
    //    Vector3 dir = _targetPosition - transform.position;
    //    if (dir.sqrMagnitude <= 2f)
    //    {
    //        ChangeState(State.Attack);
    //        return;
    //    }
    //
    //    var targetRotation = Quaternion.LookRotation(_targetPosition - transform.position, Vector3.up);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * 2f * Time.deltaTime);
    //
    //    transform.position += transform.forward * _moveSpeed * 2f * Time.deltaTime;
    //
    //}



}
