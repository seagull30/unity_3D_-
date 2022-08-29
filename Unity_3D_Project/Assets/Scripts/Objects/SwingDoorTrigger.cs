using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingDoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ActionController player = FindObjectOfType<ActionController>();
        player.UseLock();
    }
}
