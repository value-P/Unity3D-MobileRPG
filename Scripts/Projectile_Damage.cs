using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Damage : ProjectileAction
{
    public float damageValue;

    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        target?.GetDamage(proj.owner.Stat.AttackPower + damageValue, proj.owner);
    }
}

