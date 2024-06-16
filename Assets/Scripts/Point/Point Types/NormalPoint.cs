using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPoint : Point
{
    protected override void HandleTriggerCollider(Collider collision) 
    {
        base.HandleTriggerCollider(collision);
        if (collision.CompareTag("Pacman"))
        {
            UIManager.AddScore(1);
            ReleasePoint();
        }
    }
}
