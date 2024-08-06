using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot_Partner : SlotBase
{
    PartnerListWindow parent;


    protected override void Start()
    {
        base.Start();
        parent = GetComponentInParent<PartnerListWindow>();

    }

    void Update()
    {
        if (info != null)
        {
            iconImg.sprite = info.icon;

            for (int i = 0; i < GameManager.Instance.party.Length; i++)
            {
                if (GameManager.Instance.party[i] == info)
                {
                    closeImg.SetActive(true);
                    break;
                }
                else
                {
                    closeImg.SetActive(false);
                }
                
            }
        }

        if (info == GameManager.Instance.currentInfo)
        {
            selectImg.SetActive(true);
        }
        else
        {
            selectImg.SetActive(false);
        }


    }

    public void SetInfo(PartnerInfo wantInfo)
    {
        info = wantInfo;
    }

    public void SelectPartner()
    {
        // 선택한 동료의 정보가 비어있지 않을 때
        if (GameManager.Instance.currentInfo != null)
        {
            if (GameManager.Instance.currentInfo == info)
            {
                // 만약 같은 칸을 터치한거라면 null
                GameManager.Instance.currentInfo = null;
            }
            else
            {
                // 아니라면 비우고 선택한 칸의 정보를 넘기기
                GameManager.Instance.currentInfo = null;
                GameManager.Instance.currentInfo = info;
            }
        }
        else
        {
            GameManager.Instance.currentInfo = info;
        }

    }
}
