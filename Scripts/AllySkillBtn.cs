using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllySkillBtn : ButtonBase
{
    // ��Ÿ�� ǥ���� �̹���
    [SerializeField] Image coolDownImage;

    // ��ų ������
    public Image _icon;
    // ��ų�� �ߵ���ų ����
    PartnerBase _target;

    float _fillAmount = 0f;

    private void Start()
    {
        BattleSceneManager.Instance.skillBtn = this;
        _icon = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_target == null) return;

        // ��Ÿ�� ǥ��
        if (_target.equipSkill.coolTimeRate > 0)
            _fillAmount = _target.equipSkill.coolTimeRate;
        else
            _fillAmount = 0f;

        coolDownImage.fillAmount = _fillAmount;
    }

    public override void ButtonDown()
    {
        if(_target != null && _fillAmount <= 0f)
        {
            _target.SkillCasting();
            BattleSceneManager.Instance.isQTESuccess = true;
        }
    }

    public void SetTarget(PartnerBase wantTarget)
    {
        _target = wantTarget;
    }

}
