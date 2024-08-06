using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();

        // 뷰포트 랙트
        Rect rect = cam.rect;
        //                             기기의 화면 비율            /    가로 / 세로  (고정할 비율)
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float scaleWidth = 1f / scaleHeight;

        if (scaleHeight < 1)
        {
            // 기기의 화면비율이 고정할 비율보다 작을 때
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            // 기기의 화면비율이 고정할 비율보다 클 때
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        cam.rect = rect;

        // 화질이 더 작다면 Edit > Project Setting > Player > Resolution and Presentation > Render outside safe area 체크 시 16:9 비율
    }
}
