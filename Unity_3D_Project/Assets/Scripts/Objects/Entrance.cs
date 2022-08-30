using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    GameObject NearTrigger;
    GameObject ExitTrigger;
    GameObject ExitSign;

    private void Awake()
    {
        NearTrigger = transform.Find("NearTrigger").gameObject;
        ExitTrigger = transform.Find("ExitTrigger").gameObject;
        ExitSign = transform.Find("ExitSign").gameObject;
        NearTrigger.SetActive(false);
        ExitTrigger.SetActive(false);
        ExitSign.SetActive(false);
        GameManager.Instance.EscapeEvent += escpeEvent;
    }

    internal void ExitEvent()
    {
        GameManager.Instance.Ending();
    }

    void escpeEvent()
    {
        NearTrigger.SetActive(true);
        ExitTrigger.SetActive(true);
        ExitSign.SetActive(true);
    }

    internal void NearEvent()
    {
        GameManager.Instance.ExitCount += 1;

        if (GameManager.Instance.ExitCount <= 3)
        {
            Debug.Log("nonono");
            ExitTrigger.SetActive(false);
            ExitSign.SetActive(false);
        }
    }
}
