using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Party : SlotBase
{
    // �迭�� �� ��ȣ
    public int index;

    protected override void Start()
    {
        base.Start();
        closeImg.SetActive(false);
        
    }

    void Update()
    {
        if (GameManager.Instance.currentInfo != null)
        {
            selectImg.SetActive(true);
        }
        else { selectImg.SetActive(false); }
    }

    public void AddParty()
    {
        if (info == null)
        {
            // ��� �ִ� ������ ���� ������ ���ᰡ ���ٸ� return
            if (GameManager.Instance.currentInfo == null) { return; }

            // ����ִ� ������ ���ٸ� ������ ������ ������ �ű�� ������ ����� null
            info = GameManager.Instance.currentInfo;
            GameManager.Instance.currentInfo = null;
            iconImg.sprite = info.icon;
            GameManager.Instance.party[index] = info;
        }
        else
        {
            if (GameManager.Instance.currentInfo == null)
            {
                info = null;
                iconImg.sprite = null;
                GameManager.Instance.party[index] = null;
            }
            else
            {
                info = GameManager.Instance.currentInfo;
                GameManager.Instance.currentInfo = null;
                iconImg.sprite = info.icon;
                GameManager.Instance.party[index] = info;
            }
        }
    }
}
