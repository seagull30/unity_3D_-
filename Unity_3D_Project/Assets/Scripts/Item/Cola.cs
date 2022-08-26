using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cola : Use
{
    public override void UseEffect()
    {
        ProjectileManager projectileManager = FindObjectOfType<ProjectileManager>();
        projectileManager.Fire();
    }
}
