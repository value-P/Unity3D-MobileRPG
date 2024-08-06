using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot_Party : SlotBase
{
    // 배열에 들어갈 번호
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
            // 담고 있는 정보가 없고 선택한 동료가 없다면 return
            if (GameManager.Instance.currentInfo == null) { return; }

            // 담고있는 정보가 없다면 선택한 동료의 정보를 옮기고 선택한 동료는 null
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
