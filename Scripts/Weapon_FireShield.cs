using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_FireShield : WeaponBase
{
    public Weapon_FireShield(MovableBase wantOwner, float wantCoolTime) : base(wantOwner, wantCoolTime)
    {
        spawnPrefab = Resources.Load<GameObject>("Prefabs/Projectiles/FireShield");
    }

    public override void OnAttack(MovableBase wantTarget, bool wantTracking)
    {
        if (GameManager.Instance.player != null)
        {
            BuffShot(GameManager.Instance.player, true);
        }

        for (int i = 0; i < BattleSceneManager.Instance.partners.Length; i++)
        {
            if (BattleSceneManager.Instance.partners[i] != null)
            {
                BuffShot((BattleSceneManager.Instance.partners[i]), true);
            }
        }
    }
}
