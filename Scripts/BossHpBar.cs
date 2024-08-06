using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : HpBar
{
    public MonsterBase targetBoss;

    protected override void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        // Å¸°Ù¿¡¼­ ½ºÅÈ°¡Á®¿À±â
        _stat = targetBoss.Stat;
        _slider = GetComponentInChildren<Slider>();
    }

    protected override void Update()
    {
        float ratio = _stat.HPRate;
        _slider.value = ratio;

        if (ratio <= 0)
            gameObject.SetActive(false);
    }
}
