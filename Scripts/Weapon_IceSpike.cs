using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_IceSpike : WeaponBase
{
    public Weapon_IceSpike(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/IceSpike");
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        if (target != null)
        {
            Shot(target, target.transform.position, false);
        }
    }
}
