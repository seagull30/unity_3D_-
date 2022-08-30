using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.parent.GetComponent<Entrance>().NearEvent();
            gameObject.SetActive(false);
        }
    }
}
