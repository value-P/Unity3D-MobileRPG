using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Partner_LongAttack : PartnerBase
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
            currentWeapon.OnAttack(focusTarget.GetComponent<MonsterBase>(), isTracking);
        }
    }

    public void AddNormalWeapon(string wantName)
    {
        switch (wantName)
        {
            case "IceMissile":
                currentWeapon = new Weapon_IceMissile(this, 0);
                break;
            case "Fireball":
                currentWeapon = new Weapon_Fireball(this, 0);
                break;
            case "HolyMissile":
                currentWeapon = new Weapon_HolyMissile(this, 0);
                break;
                break;
        }
    }
}
