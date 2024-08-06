using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_OffCollider : ProjectileAction
{
    public Collider collider;
    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        collider.enabled = false;
    }
}
