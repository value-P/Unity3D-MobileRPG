using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_SpikeField : WeaponBase
{
    public Weapon_SpikeField(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/SpikeField");
    }

    public override void OnAttack(MovableBase target,Vector3 position, bool wantTracking)
    {
        if (target != null)
        {
            Shot(target, position, false);
        }
    }
}
