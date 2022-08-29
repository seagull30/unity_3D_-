using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject[] _collidedObject = new GameObject[3];
    int _collidedIndex = 0;
    Collider[] _targetCandidates = new Collider[3];
    private void Start()
    {

    }

    private void Update()
    {
        Move();
        if (CheckWall())
        {
            for(int i = 0; i < _collidedObject.Length; i++)
            {
                if( _collidedObject[i] != null )
                    _collidedObject[i].transform.parent = null;
            }
            DestroySelf();

        }
    }

    void Move()
    {
        transform.Translate(transform.forward * 0.5f);
    }

    bool CheckWall()
    {
        Physics.OverlapBoxNonAlloc(transform.forward , new Vector3(0.1f, 0.1f, 0.1f), _targetCandidates, Quaternion.identity);
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
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _collidedObject[_collidedIndex] = other.gameObject;
            _collidedObject[_collidedIndex].transform.parent = transform;
            _collidedIndex++;
        }
    }

}
