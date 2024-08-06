using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_AddBuff : ProjectileAction
{
    // 원하는 버프를 인스펙터창에서 조정하기
    public MovableBuff wantBuff;

    public override void Activate(ProjectileBase proj, MovableBase target, Vector3 position)
    {
        MovableBuff newBuff = new MovableBuff(wantBuff);
        target.AddBuff(newBuff);
    }
}
