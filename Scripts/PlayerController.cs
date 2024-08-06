using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayeableCharacterBase player { get => GameManager.Instance.player; } // �÷��̾�

    private void Update()
    {
        // �÷��̾ �׾��ٸ� ������� �ʵ���
        if (!player.isDead)
        {
            player.inputVector = Managers.Input.inputVector; // �Է°� �Ѱ��ֱ�
            player.isRun = Managers.Input.isRun;    // �޸��� ���� ������Ʈ
            player.Attack(Managers.Input.onAttack); // �����Է� ������ �ߵ�
        }
        else
            player.inputVector = Vector3.zero;
    }

    public void Skill(SkillType type)
    {
        // �÷��̾� ����� ��� ���ϵ���
        if(!player.isDead)
            player.Skill(type);
    }
}