using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MState
{
    protected float distance;
    public abstract void OnEnter(MonsterBase partner);

    public abstract void OnUpdate(MonsterBase partner);

    public abstract void OnExit(MonsterBase partner);
}

public class MonsterBase : AIBase
{
    MState[] states;
    MState currentState;

    public GameObject findEffect;

    public GameObject hpUI;

    // 스킬 사용을 위한 변수들
    public int hitStack;
    Vector3 skillPosition;

    protected override void Start()
    {
        base.Start();

        Setup();
    }

    protected override void Update()
    {
        base.Update();
        if (currentState != null)
        {
            currentState.OnUpdate(this);
        }
    }

    public virtual void Setup()
    {
        states = new MState[5];
        states[(int)MonsterState.Idle] = new MonsterStates.MIdle();
        states[(int)MonsterState.Walk] = new MonsterStates.MMove();
        states[(int)MonsterState.Attack] = new MonsterStates.MAttack();
        states[(int)MonsterState.Die] = new MonsterStates.MDie();

        ChangeState(MonsterState.Idle);

        findEffect = Instantiate(GameManager.findEffect, this.transform);
        findEffect.SetActive(false);

        hitStack = 0;
    }


    public override float GetDamage(float damage, MovableBase from)
    {
        base.GetDamage(damage, from);

        if (focusTarget == null) { focusTarget = from; }

        if (Stat.CurrentHp <= 0)
        {
            this.gameObject.layer = 0;
            ChangeState(MonsterState.Die);
        }

        return damage;
    }

    public void ChangeState(MonsterState newState)
    {
        if (states[(int)newState] == null) { return; }
        if (currentState != null) { currentState.OnExit(this); } 

        currentState = states[(int)newState];
        currentState.OnEnter(this);
    }

    public virtual void Anim_Attack()
    {
        switch (atkType)
        {
            case AttackType.Short:
                atkCollider.enabled = true;
                Invoke("AttackColliderOff", 0.3f);
                break;
            case AttackType.Long:
                break;
        };
    }
    void AttackColliderOff()
    {
        atkCollider.enabled = false;
    }

    public void Anim_AttackTimeCheck()
    {   
        Stat.AttackSpeed = Stat.AttackDelay;
    }

    public override void SkillCasting()
    {
        if (focusTarget != null && equipSkill.CurrentCoolTime <= 0)
        {
            GameObject picker = GameObject.Instantiate(GameManager.pickerEffect);
            picker.transform.position = focusTarget.transform.position;
            skillPosition = focusTarget.transform.position;
            GameObject.Destroy(picker, 2f);
            Invoke("SkillSpawn", 2f);
        }

        else { return; }
    }
    void SkillSpawn()
    {
        anim.SetTrigger("isSkill");
        equipSkill.CurrentCoolTime = equipSkill.CoolTime;
    }

    public void Anim_Skill()
    {
        equipSkill.OnAttack(focusTarget.GetComponent<MovableBase>(), skillPosition, skillTracking);
    }

    public override WeaponBase AddWeapon(string wantName, float wantCoolTime)
    {
        switch (wantName)
        {
            case "SpikeField":
                return new Weapon_SpikeField(this, wantCoolTime);
        }

        return null;
    }

    public override void Die()
    {
        Destroy(gameObject);
    }
}
