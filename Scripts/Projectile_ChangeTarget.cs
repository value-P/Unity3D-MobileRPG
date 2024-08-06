using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_ChangeTarget : ProjectileAction
{
    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        target.focusTarget = proj.owner;
    }
}
