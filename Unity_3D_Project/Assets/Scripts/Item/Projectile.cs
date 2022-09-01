using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Projectile : MonoBehaviour
{
    GameObject[] _collidedObject = new GameObject[3];
    int _collidedIndex = 0;
    Collider[] _targetCandidates = new Collider[3];
    private int _layerMask;
    private float _speed;

    public float X;
    public float Y;
    public float Z;

    private void Awake()
    {
        _layerMask = 1 << (LayerMask.NameToLayer("Wall"));
        _speed = 0.25f;
    }

    private void Update()
    {
        Move();
        if (CheckWall())
        {
            DestroySelf();
        }
    }

    void Move()
    {
        transform.Translate(new Vector3(0f, 0f, _speed));
    }

    bool CheckWall()
    {
        _targetCandidates = new Collider[3];
        Physics.OverlapBoxNonAlloc(transform.localPosition, new Vector3(2f, 2f, 2f), _targetCandidates, Quaternion.identity, _layerMask);
        foreach (Collider targetCandidate in _targetCandidates)
        {
            if (targetCandidate == null)
                continue;
            return true;
        }
        return false;
    }

    private void DestroySelf()
    {
        if (_collidedObject != null)
        {
            for (int i = 0; i < _collidedIndex; ++i)
            {
                _collidedObject[i].transform.parent = null;
                _collidedObject[i].GetComponent<NavMeshAgent>().isStopped = false;
            }

        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _collidedObject[_collidedIndex] = other.gameObject;
            _collidedObject[_collidedIndex].GetComponent<NavMeshAgent>().isStopped = true;
            _collidedObject[_collidedIndex].transform.parent = transform;
            _collidedIndex++;
        }
    }

}
