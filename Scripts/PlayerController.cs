using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayeableCharacterBase player { get => GameManager.Instance.player; } // 플레이어

    private void Update()
    {
        // 플레이어가 죽었다면 실행되지 않도록
        if (!player.isDead)
        {
            player.inputVector = Managers.Input.inputVector; // 입력값 넘겨주기
            player.isRun = Managers.Input.isRun;    // 달리기 상태 업데이트
            player.Attack(Managers.Input.onAttack); // 공격입력 받으면 발동
        }
        else
            player.inputVector = Vector3.zero;
    }

    public void Skill(SkillType type)
    {
        // 플레이어 사망시 사용 못하도록
        if(!player.isDead)
            player.Skill(type);
    }
}