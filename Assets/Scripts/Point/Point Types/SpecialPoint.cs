using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPoint : Point
{
    protected override void HandleTriggerCollider(Collider collision) 
    {
        base.HandleTriggerCollider(collision);
        if (collision.CompareTag("Pacman"))
        {
            UIManager.AddScore(2);
            ReleasePoint();
        }
    }
}
