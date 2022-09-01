using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedVision : MonoBehaviour
{
    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
        GameManager.Instance.EscapeEvent += RedEvent;
    }

    private void RedEvent()
    {
        _image.enabled = true;
    }
}
