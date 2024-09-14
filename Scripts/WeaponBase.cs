using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase
{
    protected MovableBase owner;
    protected GameObject spawnPrefab;

    // 해당 스킬의 쿨타임
    protected float _coolTime;
    public float CoolTime
    {
        get => _coolTime;
        set
        {
            _coolTime = value;
        }
    }
    // 해당 스킬의 현재 쿨타임
    protected float _currentCoolTime;
    public float CurrentCoolTime
    {
        get => _currentCoolTime;
        set => _currentCoolTime = Mathf.Clamp(value, 0, CoolTime);
    }
    // 해당 스킬의 쿨타임 비율
    public float coolTimeRate { get => CurrentCoolTime / CoolTime; }

    public WeaponBase(MovableBase wantOwner, float wantCoolTime)
    {
        owner = wantOwner;
        CoolTime = wantCoolTime;
        CurrentCoolTime = 0;
    }

    protected virtual ProjectileBase Shot(MovableBase wanttarget, Vector3 wantPosition, bool wantTracking)
    {
        ProjectileBase proj = GameObject.Instantiate(spawnPrefab).GetComponent<ProjectileBase>();
        proj.Initialize(owner, wanttarget, wantTracking);
        wantPosition.y += 0.5f;
        proj.transform.position = wantPosition;

        return proj;
    }

    protected virtual ProjectileBase BuffShot(MovableBase wantTarget, bool isBuff)
    {
        ProjectileBase proj = GameObject.Instantiate(spawnPrefab).GetComponent<ProjectileBase>();
        proj.BuffInitialize(owner, wantTarget, isBuff);
        proj.transform.position = wantTarget.transform.position;
        return proj;
    }

    public virtual void OnAttack(MovableBase target, bool wantTracking) { }
    public virtual void OnAttack() { }
    public virtual void OnAttack(MovableBase target, Vector3 wantPosition, bool wantTracking) { }


}
