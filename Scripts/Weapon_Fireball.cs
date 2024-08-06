using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Fireball : WeaponBase
{
    public Weapon_Fireball(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/Fireball");
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(target, owner.transform.position, wantTracking);
    }
}
