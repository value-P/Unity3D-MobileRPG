using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_IceMissile : WeaponBase
{
    public Weapon_IceMissile(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        // 오브젝트 할당
       spawnPrefab= Resources.Load<GameObject>("Prefabs/Projectiles/IceMissile");
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        // 해당 스킬은 발사체이기 때문에 정보를 넘겨주면서 바로 발사체를 소환한다.
        Shot(target, owner.transform.position, wantTracking);
    }
}
