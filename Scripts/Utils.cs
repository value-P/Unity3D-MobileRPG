using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    // go�� TŸ�� ������Ʈ�� �ִٸ� ��ȯ�ϰ� ���ٸ� �ٿ��� ��ȯ����
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }
}