using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 키보드, 마우스, 터치를 이벤트로 오브젝트에 보낼 수 있는 기능 지원

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)]
    private float leverRange;

    private Vector2 inputDirection;
    private bool isInput;

    // 너비 절반
    float widthHalf;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        widthHalf = rectTransform.sizeDelta.x * 0.5f;
    }

    // 드래그 시작시
    public void OnBeginDrag(PointerEventData eventData)
    {
        JoyStickControll(eventData);
        isInput = true;
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        JoyStickControll(eventData);
    }

    // 드래그 끝낼때
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
    }

    Vector2 inputPos;
    Vector2 inputVector;
    private void JoyStickControll(PointerEventData eventData)
    {
        // 조이스틱이 앵커에서 떨어진 만큼 빼서 계산을 조이스틱 중심으로 할 수 있도록 한다 && 정규화(0~1)사이로
        inputPos = (eventData.position - rectTransform.anchoredPosition) / widthHalf;
        inputVector = inputPos.magnitude < 1 ? inputPos : inputPos.normalized;
        lever.anchoredPosition = inputVector * widthHalf;
        inputDirection = inputVector; // 스틱의 움직인 양을 0 ~ 1사이로 정규화
    }

    private void Update()
    {
        if (isInput)
            Managers.Input.inputVector = inputDirection;
        else
            Managers.Input.inputVector = Vector2.zero;
    }
}

