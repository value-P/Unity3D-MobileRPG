using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : ButtonBase
{
    [SerializeField] SkillType type;      // 무슨 스킬의 버튼인지
    [SerializeField] Image coolDownImage; // 쿨타임 그림자

    float _fillAmount = 0f;

    private void Update()
    {
        if(GameManager.Instance.player != null)
        {
            switch(type)
            {
                case SkillType.Normal:
                    if (GameManager.Instance.player.equipSkill.coolTimeRate > 0)
                    {
                        _fillAmount = GameManager.Instance.player.equipSkill.coolTimeRate;
                    }
                    else
                        _fillAmount = 0;
                    break;

                case SkillType.Ultimate:
                    if (GameManager.Instance.player.ultimateSkill.coolTimeRate > 0)
                    {
                        _fillAmount = GameManager.Instance.player.ultimateSkill.coolTimeRate;
                    }
                    else
                        _fillAmount = 0;
                    break;
            }

            coolDownImage.fillAmount = _fillAmount;
        }
    }

    public void Click()
    {
        if(_fillAmount <= 0f)
        {
            GameManager.Instance.player.Skill(type);

            // 궁극기 사용시 QTE발동
            if (type == SkillType.Ultimate)
                BattleSceneManager.Instance.StartQTE();
        }
    }

}
