using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_FloatingSword : WeaponBase
{
    public Weapon_FloatingSword(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>($"Prefabs/Skills/FloatingSword");
    }

    protected override ProjectileBase Shot(MovableBase wanttarget, Vector3 wantPosition, bool wantTracking)
    {
        // 스킬 오브젝트 소환하고 위치 할당
        ProjectileBase proj = GameObject.Instantiate(spawnPrefab, owner.transform).GetComponent<ProjectileBase>();
        proj.Initialize(owner);
        Vector3 wantPos = Vector3.zero;
        wantPos.y += 0.7f;
        proj.transform.position = owner.transform.position + wantPos;

        return proj;
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        Shot(target, owner.transform.position, wantTracking);
    }

}
