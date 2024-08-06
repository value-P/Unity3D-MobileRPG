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

    Vector3 startPos; // 원래 위치
    float shakeTime; // 체크용 시간
    IEnumerator ShakeCoroutine()
    {
        startPos = transform.position;
        shakeTime = _shakeTime;

        while(shakeTime > 0f)
        {
            // 초기 위치로부터 구 범위(Size1) * 흔들기 강도 만큼 카메라 위치 변동
            transform.position = startPos + Random.insideUnitSphere * _shakeIntensity;

            shakeTime -= Time.deltaTime;
        
            yield return null;
        }

        transform.position = startPos;
    }
}
