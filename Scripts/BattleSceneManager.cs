using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    private static BattleSceneManager _instance;
    public static BattleSceneManager Instance { get => _instance; }

    public PartnerBase[] partners = new PartnerBase[3];
    public Transform spawnPosition;

    public Image[] partnersHp = new Image[3];

    public AllySkillBtn skillBtn;
    Sprite[] skillIcons = new Sprite[3];

    public float qteTime;
    public bool isQTESuccess = false;

    bool isQTEStarted = false;
    float qteTimer;
    int qteIdx;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        Initialize();
    }

    private void Initialize()
    {
        GameManager.Instance.SpawnPlayerCharacter();
        GameManager.Instance.player.gameObject.transform.position = spawnPosition.position;

        for (int i = 0; i < partners.Length; i++)
        {
            PartnerInfo currentInfo = GameManager.Instance.party[i];
            partners[i] = Instantiate(currentInfo.prefab).GetComponent<PartnerBase>();
            partners[i].gameObject.SetActive(false);
            partnersHp[i].sprite = currentInfo.icon;

            Vector3 spawnPos = spawnPosition.position;
            spawnPos.z -= 1f;
            switch (i)
            {
                case 1:
                    spawnPos.x -= 1f;
                    break;
                case 2:
                    spawnPos.x += 1f;
                    break;
                default:
                    break;
            }

            partners[i].gameObject.transform.position = spawnPos;

            partners[i].gameObject.SetActive(true);
            skillIcons[i] = currentInfo.icon;
        }
    }

    private void Update()
    {
        if (isQTEStarted)
        {
            qteTimer += Time.deltaTime; 

            if (qteTimer > qteTime)
            {
                isQTEStarted = false; 
                skillBtn.gameObject.SetActive(false); 
            }

            if (isQTESuccess)
            {
                qteTimer = 0f;
                if (qteIdx >= partners.Length - 1)
                {
                    isQTEStarted = false; 
                    skillBtn.gameObject.SetActive(false); 
                    return; 
                }
                else
                {
                    isQTESuccess = false;

                    qteIdx++;
                    while (partners[qteIdx] == null)
                    {
                        qteIdx++;
                        if (qteIdx >= partners.Length)
                            return;
                    }

                    skillBtn.gameObject.SetActive(false);
                    skillBtn._icon.sprite = skillIcons[qteIdx];
                    skillBtn.SetTarget(partners[qteIdx]);
                    skillBtn.gameObject.SetActive(true);
                }
            }
        }

        for (int i = 0; i < partners.Length; i++)
        {
            if (partners[i] != null)
            {
                partners[i].idlePosition = GameManager.Instance.player.partnerPos[i];
            }
        }
    }

    public void StartQTE()
    {
        qteIdx = 0;

        while (partners[qteIdx] == null)
        {
            qteIdx++;
            if (qteIdx >= partners.Length)
                return;
        }

        if (partners[qteIdx].focusTarget == null) return;

        isQTEStarted = true;
        qteTimer = 0f;
        isQTESuccess = false;

        skillBtn._icon.sprite = skillIcons[qteIdx];
        skillBtn.SetTarget(partners[qteIdx]);
        skillBtn.gameObject.SetActive(true);
    }

}
