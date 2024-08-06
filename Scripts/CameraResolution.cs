using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    private void Awake()
    {
        Camera cam = GetComponent<Camera>();

        // ����Ʈ ��Ʈ
        Rect rect = cam.rect;
        //                             ����� ȭ�� ����            /    ���� / ����  (������ ����)
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)16 / 9);
        float scaleWidth = 1f / scaleHeight;

        if (scaleHeight < 1)
        {
            // ����� ȭ������� ������ �������� ���� ��
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            // ����� ȭ������� ������ �������� Ŭ ��
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }
        cam.rect = rect;

        // ȭ���� �� �۴ٸ� Edit > Project Setting > Player > Resolution and Presentation > Render outside safe area üũ �� 16:9 ����
    }
}
