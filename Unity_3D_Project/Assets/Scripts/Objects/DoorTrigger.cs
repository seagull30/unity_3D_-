using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       transform.parent.GetComponent<Door>().OnTriggerEnterInChild(other);
    }
}
