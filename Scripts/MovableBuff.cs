using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable] // �ν�����â���� ������ ������ġ���� ������ �� �ְ� �����
public class MovableBuff
{
    // �ش� ������ ����
    public BuffType type;
    // �ش� ������ ���� ��ġ
    public float buffValue;
    // �ش� ������ ���ӽð�
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
