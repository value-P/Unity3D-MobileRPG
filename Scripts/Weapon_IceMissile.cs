using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_IceMissile : WeaponBase
{
    public Weapon_IceMissile(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        // ������Ʈ �Ҵ�
       spawnPrefab= Resources.Load<GameObject>("Prefabs/Projectiles/IceMissile");
    }

    public override void OnAttack(MovableBase target, bool wantTracking)
    {
        // �ش� ��ų�� �߻�ü�̱� ������ ������ �Ѱ��ָ鼭 �ٷ� �߻�ü�� ��ȯ�Ѵ�.
        Shot(target, owner.transform.position, wantTracking);
    }
}
