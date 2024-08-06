using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    // 자신의 rectTransform
    protected RectTransform _rectTransform;
    // 부모 트랜스폼
    protected Transform parent; 
    // 받아올 스탯
    protected StatBase _stat;
    // 체력의 크기 보여줄 슬라이더
    protected Slider _slider;
    // 부모 콜라이더 (높이를 받기 위해)
    protected Collider _col;


    protected virtual void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        parent = transform.parent;
        // 부모에서 스탯가져오기
        _stat = parent.GetComponent<StatBase>();
        _col = parent.GetComponent<Collider>();
        _slider = GetComponentInChildren<Slider>();
    }

    protected virtual void Update()
    {
        // 주인의 머리 위에 뜨도록
        _rectTransform.anchoredPosition = Vector3.up * (2f);

        // UI의 회전방향을 카메라의 회전방향과 같도록 
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.HPRate;
        _slider.value = ratio;
    }
}
