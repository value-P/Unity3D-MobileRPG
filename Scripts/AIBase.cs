using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class AIBase : MovableBase
{
    public AttackType atkType; 

    public MovableBase oldFocusTarget;

    [SerializeField]
    protected float _atkRange = 0f;
    public float AtkRange
    {
        get => _atkRange;
        set
        {
            _atkRange = value;
        }
    }

    [SerializeField]
    protected float _findRange = 0f;
    public float FindRange
    {
        get => _findRange;
        set
        {
            _findRange = value;
        }
    }
    public NavMeshAgent agent;

    public bool skillTracking;
    
    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.stat.MoveSpeed; 
    }

    public override float GetDamage(float damage, MovableBase from)
    {
        if (buffs.Count < 1) { StartCoroutine(GetDamageMeshs()); }


        damage -= Stat.DefensePower;                              
        damage = Mathf.Max(0, damage);                             
        if (Stat.CurrentHp < damage) { damage = Stat.CurrentHp; }  
        if (damage == 0) { return 0; }

        Stat.CurrentHp -= damage;                                  

        return damage;
    }
    public IEnumerator GetDamageMeshs()
    {
        foreach (SkinnedMeshRenderer mesh in meshs) { mesh.material.color = Color.red; }
        yield return new WaitForSeconds(0.25f);

        foreach (SkinnedMeshRenderer mesh in meshs) { mesh.material.color = Color.white; }
        yield break;
    }

    public virtual void SkillCasting()
    {
        if (focusTarget != null && equipSkill.CurrentCoolTime <= 0)
        {
            anim.SetTrigger("isSkill");
            equipSkill.CurrentCoolTime = equipSkill.CoolTime;
        }
        else { return; }
    }

    public virtual void OffCollider()
    {
        collider.enabled = false;
    }
    
    abstract public void Die();
}
