using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_AcidMissile : WeaponBase
{
    public Weapon_AcidMissile(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/AcidMissile");
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(target, owner.transform.position, wantTracking);
    }
}
