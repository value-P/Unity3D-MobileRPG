using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] // 인스펙터창에서 버프의 정보수치들을 조절할 수 있게 만들기
public class MovableBuff
{
    // 해당 버프의 종류
    public BuffType type;
    // 해당 버프의 조정 수치
    public float buffValue;
    // 해당 버프의 지속시간
    public float leftTime;

    public MovableBuff(BuffType wantType, float wantValue, float wantTime)
    {
        type = wantType;
        buffValue = wantValue;
        leftTime = wantTime;
    }

    public MovableBuff(MovableBuff from)
    {
        type=from.type;
        buffValue=from.buffValue;
        leftTime=from.leftTime;
    }

    public Action<MovableBase> BuffStart
    {
        get
        {
            switch (type)
            {
                case BuffType.Speed: return SpeedStart;
                case BuffType.Defense: return DefenseStart;
                case BuffType.Heal: return HealStart;
                default: return null;
            }
        }
    }

    public Action<MovableBase> BuffExit
    {
        get
        {
            switch (type)
            {
                case BuffType.Speed: return SpeedExit;
                case BuffType.Defense: return DefenseExit;
                case BuffType.Heal: return HealExit;
                default: return null;
            }
        }
    }

    void SpeedStart(MovableBase target)
    {
        target.anim.speed -= buffValue;
        foreach (SkinnedMeshRenderer mesh in target.meshs) { mesh.material.color = Color.blue; }
    }
    void SpeedExit(MovableBase target)
    {
        target.anim.speed += buffValue;
        foreach (SkinnedMeshRenderer mesh in target.meshs) { mesh.material.color = Color.white; }
    }

    void DefenseStart(MovableBase target)
    {
        target.Stat.DefensePower += buffValue;
    }
    void DefenseExit(MovableBase target)
    {
        target.Stat.DefensePower -= buffValue;
    }
    void HealStart(MovableBase target)
    {
        target.Stat.CurrentHp += buffValue;
    }
    void HealExit(MovableBase target)
    {
        
    }
}
