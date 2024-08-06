using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_HolyMissile : WeaponBase
{
    public Weapon_HolyMissile(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/HolyMissile");
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(target, owner.transform.position, wantTracking);
    }
}
