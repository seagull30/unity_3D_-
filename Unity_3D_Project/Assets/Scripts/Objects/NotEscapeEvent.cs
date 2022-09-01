using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEscapeEvent : MonoBehaviour
{
    private void Awake()
    {
        transform.parent.parent.GetComponent<Entrance>().NotEscape += Down;
    }

    private void Down()
    {
        transform.localPosition -= new Vector3(0f, 10f, -10f);

    }
}
