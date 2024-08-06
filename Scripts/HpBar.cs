using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    // �ڽ��� rectTransform
    protected RectTransform _rectTransform;
    // �θ� Ʈ������
    protected Transform parent; 
    // �޾ƿ� ����
    protected StatBase _stat;
    // ü���� ũ�� ������ �����̴�
    protected Slider _slider;
    // �θ� �ݶ��̴� (���̸� �ޱ� ����)
    protected Collider _col;


    protected virtual void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        parent = transform.parent;
        // �θ𿡼� ���Ȱ�������
        _stat = parent.GetComponent<StatBase>();
        _col = parent.GetComponent<Collider>();
        _slider = GetComponentInChildren<Slider>();
    }

    protected virtual void Update()
    {
        // ������ �Ӹ� ���� �ߵ���
        _rectTransform.anchoredPosition = Vector3.up * (2f);

        // UI�� ȸ�������� ī�޶��� ȸ������� ������ 
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.HPRate;
        _slider.value = ratio;
    }
}
