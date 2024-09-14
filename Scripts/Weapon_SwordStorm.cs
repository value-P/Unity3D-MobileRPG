using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_SwordStorm : WeaponBase
{
    public Weapon_SwordStorm(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>($"Prefabs/Skills/SwordStorm");
    }

    protected override ProjectileBase Shot(MovableBase wanttarget, Vector3 wantPosition, bool wantTracking)
    {
        ProjectileBase proj = GameObject.Instantiate(spawnPrefab).GetComponent<ProjectileBase>();
        proj.Initialize(owner);
        proj.transform.position = wantPosition;
        proj.transform.rotation = Quaternion.LookRotation(owner.transform.forward);

        return proj;
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(null, owner.transform.position, false);
    }

}
