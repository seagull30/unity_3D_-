using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snack : Use
{
    public override void UseEffect()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.UseSnack();
    }
}
