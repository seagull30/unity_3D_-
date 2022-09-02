using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTitle : MonoBehaviour
{
    private GameObject _main;
    private GameObject _controls;

    private void Awake()
    {
        _main = transform.Find("Main").gameObject;
        _controls = transform.Find("Controls").gameObject;
        _controls.SetActive(false);
    }

    public void ShowMain()
    {
        _controls.SetActive(false);
        _main.SetActive(true);
    }

    public void showControls()
    {
        _main.SetActive(false);
        _controls.SetActive(true);
    }
}
