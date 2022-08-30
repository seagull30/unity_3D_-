using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwingDoor : Door
{
    public Material _lockMaterial;
    private NavMeshObstacle _obstacle;
    private float _lockTime = 15f;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _inside = transform.Find("_DoorIn").gameObject.GetComponent<MeshRenderer>();
        _outside = transform.Find("_DoorOut").gameObject.GetComponent<MeshRenderer>();
        _barrier = _inside.GetComponent<MeshCollider>();
        _barrier.enabled = false;
        _obstacle = GetComponentInChildren<NavMeshObstacle>();
        _obstacle.enabled = false;
    }

    public void LockDoor()
    {
        _barrier.enabled = true;
        _obstacle.enabled = true;
        IsLock = true;
        _inside.sharedMaterial = _lockMaterial;
        _outside.sharedMaterial = _lockMaterial;
        Invoke("UnlockDoor", _lockTime);
    }

    private void UnlockDoor()
    {
        _barrier.enabled = false;
        _obstacle.enabled = false;
        IsLock = false;
        _inside.sharedMaterial = _closeMaterial;
        _outside.sharedMaterial = _closeMaterial;
    }


}
