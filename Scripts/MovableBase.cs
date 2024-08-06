using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatBase))]
public abstract class MovableBase : MonoBehaviour
{
    protected StatBase stat;
    public StatBase Stat
    {
        set => stat = value;
        get
        {
            if (!stat)
            {
                stat = GetComponent<StatBase>();
            }
            return stat;
        }
    }
    protected Collider collider;        // �ݶ��̴�
    public Animator anim;               // �ִϸ�����
    public MovableBase focusTarget;     // �� ĳ������ ��ǥ��
    public bool isAlly;                 // ���Ϳ���
    public SkinnedMeshRenderer[] meshs;

    public Collider atkCollider; // 근접공격에 필요한 콜라이더
    public WeaponBase equipSkill;
    public string skillName;
    public float skillCoolTime;

    // 함수를 담아두는 변수
    protected System.Action MovableUpdate;
    // 버프 목록
    protected List<MovableBuff> buffs = new List<MovableBuff>();

    abstract public float GetDamage(float damage, MovableBase from);   // �ǰݰ��� �޼���

    protected virtual void Start()
    {
        stat = GetComponent<StatBase>();
        collider = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>();

        equipSkill = AddWeapon(skillName, skillCoolTime);
        MovableUpdate += CoolTimeCycle;
        MovableUpdate += BuffUpdate;
    }

    protected virtual void Update()
    {
        if (MovableUpdate != null) { MovableUpdate(); }
    }

    public virtual WeaponBase AddWeapon(string wantName, float wantCoolTime) { return null; }

    void BuffUpdate()
    {
        for (int i = 0; i < buffs.Count; i++)
        {
            MovableBuff currentBuff = buffs[i];

            currentBuff.leftTime -= Time.deltaTime;

            // 버프 지속시간이 다된다면 버프를 끝내고 다시 앞번호로 땡기기(지워졌으니 뒷번호 버프가 앞으로 당겨지기 때문에)
            if (currentBuff.leftTime <= 0)
            {
                RemoveBuff(currentBuff);
                --i;
            }
        }
    }

    void CoolTimeCycle()
    {
        if(equipSkill != null && equipSkill.CurrentCoolTime > 0)
        {
            equipSkill.CurrentCoolTime -= Time.deltaTime;
        }
    }

    public void AddBuff(MovableBuff wantBuff)
    {
        buffs.Add(wantBuff);

        if (wantBuff.BuffStart != null) { wantBuff.BuffStart(this); }
    }

    public void RemoveBuff(MovableBuff wantBuff)
    {
        buffs.Remove(wantBuff);

        if (wantBuff.BuffExit != null) { wantBuff.BuffExit(this); }
    }
}
