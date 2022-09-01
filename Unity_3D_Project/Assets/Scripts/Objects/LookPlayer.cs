using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour
{
    void Update()
    {
        transform.rotation.SetLookRotation(GameManager.Instance.Player.transform.position);
    }
}
