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
    // 체력 게이지
    Slider _slider;
    // 참조할 캐릭터의 스탯
    StatBase _stat;
    // 무슨캐릭터의 체력을 보여줄 것인지
    [Tooltip("무슨캐릭터의 체력을 보여줄 것인지")]
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

    // 참조할 스탯 할당
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
