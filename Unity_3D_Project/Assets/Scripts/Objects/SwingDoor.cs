using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwingDoor : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip _openSound;
    public AudioClip _closeSound;
    public Material _openMaterial;
    public Material _closeMaterial;
    public Material _lockMaterial;
    private MeshRenderer _inside;
    private MeshRenderer _outside;
    private MeshCollider _barrier;
    private NavMeshObstacle _obstacle;
    private float _lockTime = 15f;
    private bool _isOpen = false;
    public bool IsLock = false;

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

    public void OnTriggerEnterInChild(Collider other)
    {
        if (!_isOpen)
        {
            _isOpen = true;
            _audioSource.PlayOneShot(_openSound);
            OpenDoor();
            if (other.tag == "Player")
            {
                GameManager.Instance.Player.GetComponentInChildren<ActionController>().PlayerShoutOut();
            }
            Invoke("CloseDoor", 3f);
        }
    }

    void OpenDoor()
    {
        _isOpen = true;
        _audioSource.PlayOneShot(_openSound);

        _inside.sharedMaterial = _openMaterial;
        _outside.sharedMaterial = _openMaterial;
    }

    void CloseDoor()
    {
        _isOpen = false;
        _audioSource.PlayOneShot(_closeSound);
        _inside.sharedMaterial = _closeMaterial;
        _outside.sharedMaterial = _closeMaterial;
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
        _inside.sharedMaterial = _openMaterial;
        _outside.sharedMaterial = _openMaterial;
    }


}
