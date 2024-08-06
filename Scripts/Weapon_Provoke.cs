using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Provoke : WeaponBase
{
    public Weapon_Provoke(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/Provoke");
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(null, owner.transform.position, wantTracking);
    }
}
