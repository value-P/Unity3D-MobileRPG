using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Ű����, ���콺, ��ġ�� �̺�Ʈ�� ������Ʈ�� ���� �� �ִ� ��� ����

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector2 inputDirection;
    private bool isInput;

    // �ʺ� ����
    float widthHalf;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        widthHalf = rectTransform.sizeDelta.x * 0.5f;
    }

    // �巡�� ���۽�
    public void OnBeginDrag(PointerEventData eventData)
    {
        JoyStickControll(eventData);
        isInput = true;
    }

    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        JoyStickControll(eventData);
    }

    // �巡�� ������
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
    }

    Vector2 inputPos;
    Vector2 inputVector;
    private void JoyStickControll(PointerEventData eventData)
    {
        // ���̽�ƽ�� ��Ŀ���� ������ ��ŭ ���� ����� ���̽�ƽ �߽����� �� �� �ֵ��� �Ѵ� && ����ȭ(0~1)���̷�
        inputPos = (eventData.position - rectTransform.anchoredPosition) / widthHalf;
        inputVector = inputPos.magnitude < 1 ? inputPos : inputPos.normalized;
        lever.anchoredPosition = inputVector * widthHalf;
        inputDirection = inputVector; // ��ƽ�� ������ ���� 0 ~ 1���̷� ����ȭ
    }

    private void Update()
    {
        if (isInput)
            Managers.Input.inputVector = inputDirection;
        else
            Managers.Input.inputVector = Vector2.zero;
    }
}

