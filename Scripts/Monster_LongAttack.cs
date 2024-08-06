using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_LongAttack : MonsterBase
{
    // 캐릭터가 가지고 있는 스킬
    WeaponBase currentWeapon;

    // 원하는 스킬의 이름
    public string weaponName;

    // 타겟팅 여부, true 라면 발사체가 적을 계속 추적한다.
    public bool isTracking;

    protected override void Start()
    {
        base.Start();

        // 스킬 할당
        AddNormalWeapon(weaponName);

    }
    protected override void Update()
    {
        base.Update();
    }


    public override void Anim_Attack()
    {
        if (focusTarget == null) { return; }
        if (atkType == AttackType.Long)
        {
            // 해당 스킬이 가지고 있는 역할 실행
            currentWeapon.OnAttack(focusTarget.GetComponent<MovableBase>(), isTracking);
        }
    }

    public void AddNormalWeapon(string wantName)
    {
        switch (wantName)
        {
            case "AcidMissile":
                currentWeapon = new Weapon_AcidMissile(this, 0);
                break;
        }
    }
}
