using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Use
{
    RaycastHit _hitinfo;
    private int _layerMask;

    public override void UseEffect()
    {
        _layerMask = 1 << (LayerMask.NameToLayer("Door"));
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hitinfo, 10f, _layerMask))
        {
            if (_hitinfo.transform != null)
            {

                SwingDoor door = _hitinfo.transform.parent.GetComponent<SwingDoor>();
                if (!door.IsLock)
                    door.LockDoor();

            }
        }


    }
}
