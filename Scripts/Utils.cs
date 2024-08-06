using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    // go에 T타입 컴포넌트가 있다면 반환하고 없다면 붙여서 반환해줌
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }
}
