using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_LongAttack : MonsterBase
{
    // ĳ���Ͱ� ������ �ִ� ��ų
    WeaponBase currentWeapon;

    // ���ϴ� ��ų�� �̸�
    public string weaponName;

    // Ÿ���� ����, true ��� �߻�ü�� ���� ��� �����Ѵ�.
    public bool isTracking;

    protected override void Start()
    {
        base.Start();

        // ��ų �Ҵ�
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
            // �ش� ��ų�� ������ �ִ� ���� ����
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
