using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float _shakeTime = 1.0f;
    [SerializeField] float _shakeIntensity = 0.1f;

    public void OnShakeCamera()
    {
        StartCoroutine(ShakeCoroutine());
    }

    Vector3 startPos; // ���� ��ġ
    float shakeTime; // üũ�� �ð�
    IEnumerator ShakeCoroutine()
    {
        startPos = transform.position;
        shakeTime = _shakeTime;

        while(shakeTime > 0f)
        {
            // �ʱ� ��ġ�κ��� �� ����(Size1) * ���� ���� ��ŭ ī�޶� ��ġ ����
            transform.position = startPos + Random.insideUnitSphere * _shakeIntensity;

            shakeTime -= Time.deltaTime;
        
            yield return null;
        }

        transform.position = startPos;
    }
}
