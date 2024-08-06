using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAttack : MonoBehaviour
{
    MovableBase owner;
    Collider collider;
    GameObject effect;

    void Start()
    {
        collider = GetComponent<Collider>();
        if (owner == null)
        {
            // ������ ã�� �� ���ο��� �ݶ��̴� ������ �Ҵ�
            owner = GetComponentInParent<MovableBase>();
            owner.atkCollider = collider;
            collider.enabled = false;
        }
        if (owner.isAlly) { effect = Instantiate(GameManager.swordEffect, collider.transform); }
        else { effect = Instantiate(GameManager.monsterAtkEffect, collider.transform); }

        effect.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        // ����ó��.ĳ���Ͱ� �ƴ϶�� return
        if(other.GetComponent<MovableBase>() == null) { return; }
        // ĳ���� �϶��� �浹 �̺�Ʈ �Լ� �����ϰ� ����
        else
        {
            Activate(other.GetComponent<MovableBase>());
        }
    }

    void Activate(MovableBase wantTarget)
    {
        if(wantTarget == null) { return; }

        if(wantTarget.isAlly != owner.isAlly)
        {
            effect.SetActive(true);
            wantTarget.GetDamage(owner.Stat.AttackPower, owner);
        }
    }
}
