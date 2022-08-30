using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    public AudioClip _openSound;
    public AudioClip _closeSound;
    public Material _openMaterial;
    public Material _closeMaterial;
    protected AudioSource _audioSource;
    protected MeshRenderer _inside;
    protected MeshRenderer _outside;
    protected MeshCollider _barrier;
    public bool _isOpen = false;
    public bool IsLock = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _inside = transform.Find("_DoorIn").gameObject.GetComponent<MeshRenderer>();
        _outside = transform.Find("_DoorOut").gameObject.GetComponent<MeshRenderer>();
        _barrier = _inside.GetComponent<MeshCollider>();
        _barrier.enabled = false;

    }

    public virtual void OnTriggerEnterInChild(Collider other)
    {
        if (!_isOpen && !IsLock)
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

    public virtual void OpenDoor()
    {
        _isOpen = true;
        _audioSource.PlayOneShot(_openSound);   

        _inside.sharedMaterial = _openMaterial;
        _outside.sharedMaterial = _openMaterial;
    }

    public virtual void CloseDoor()
    {
        _isOpen = false;
        if(!IsLock)
        {
        _audioSource.PlayOneShot(_closeSound);
        _inside.sharedMaterial = _closeMaterial;
        _outside.sharedMaterial = _closeMaterial;
        }
    }
}
