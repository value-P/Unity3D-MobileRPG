using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_AddBuff : ProjectileAction
{
    // ���ϴ� ������ �ν�����â���� �����ϱ�
    public MovableBuff wantBuff;

    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        MovableBuff newBuff = new MovableBuff(wantBuff);
        target.AddBuff(newBuff);
    }
}
