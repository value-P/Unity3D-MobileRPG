using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType
{
    Partner1,
    Partner2,
    Partner3,
    Player
}

public class AllyHpBar : MonoBehaviour
{
    // ü�� ������
    Slider _slider;
    // ������ ĳ������ ����
    StatBase _stat;
    // ����ĳ������ ü���� ������ ������
    [Tooltip("����ĳ������ ü���� ������ ������")]
    [SerializeField] CharacterType type;

    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        SetOwner(type);
    }

    void Update()
    {
        _slider.value = _stat.HPRate;
    }

    // ������ ���� �Ҵ�
    void SetOwner(CharacterType wantType)
    {
        if(wantType == CharacterType.Player)
        {
            _stat = GameManager.Instance.player.Stat;
        }
        else
        {
            _stat = BattleSceneManager.Instance.partners[(int)wantType].Stat;
        }
    }
}
