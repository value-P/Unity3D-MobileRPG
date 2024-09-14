using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers _instance = null;
    public static Managers Instance { get => _instance; }

    private static InputManager _input = new InputManager();

    public static InputManager Input { get { Init(); return _input; } }

    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        // �̱���
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject("@Managers");

            _instance = Utils.GetOrAddComponent<Managers>(go);

            DontDestroyOnLoad(go);

            _input.Init();

            Application.targetFrameRate = 60;
        }
    }
}