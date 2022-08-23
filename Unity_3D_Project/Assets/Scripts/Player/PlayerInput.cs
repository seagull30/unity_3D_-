using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float MouseX { get; private set; }
    public float MouseY { get; private set; }
    public bool Jump { get; private set; }
    public bool Interaction { get; private set; }
    public bool Run { get; private set; }
    public bool useItem { get; private set; }
    public float selectItem { get; private set; }

    private void Update()
    {
        X = Input.GetAxisRaw("Horizontal");
        Y = Input.GetAxisRaw("Vertical");
        MouseX = Input.GetAxisRaw("Mouse X");
        MouseY = Input.GetAxisRaw("Mouse Y");
        Jump = Input.GetKeyDown(KeyCode.Space);
        Run = Input.GetKey(KeyCode.LeftShift);
        Interaction = Input.GetMouseButtonDown(0);
        useItem =   Input.GetKeyDown(KeyCode.E);
        selectItem = Input.GetAxisRaw("Mouse ScrollWheel");       
    }
}
