using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosableUI : MonoBehaviour
{
    // 해당 창의 이름
    public string windowName;
    // 해당 창이 열려있는가?
    public bool isOpen;

    public static Dictionary<string, ClosableUI> uiDic = new Dictionary<string, ClosableUI>();

    Animator anim;

    public static void OpenUI(string wantName)
    {
        if (uiDic.ContainsKey(wantName))
        {
            uiDic[wantName].PlayOpen();
        }
    }

    public static void CloseUI(string wantName)
    {
        if (uiDic.ContainsKey(wantName))
        {
            uiDic[wantName].PlayClose();
        }
    }

    public void PlayOpen()
    {
        gameObject.SetActive(true);
    }

    public void PlayClose()
    {
        if (anim) { anim.SetTrigger("isClose"); }
        else { Disable(); }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        anim = GetComponent<Animator>();

        if (!uiDic.ContainsKey(windowName))
        {
            uiDic.Add(windowName, this);
        }

        if (!isOpen)
        {
            Invoke("Disable", 0.3f);
        }
    }
}
