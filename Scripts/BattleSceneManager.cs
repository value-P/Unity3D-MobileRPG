using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    private static BattleSceneManager _instance;
    public static BattleSceneManager Instance { get => _instance; }

    // ���� ���� â���� ������ ���� �迭 
    public PartnerBase[] partners = new PartnerBase[3];
    public Transform spawnPosition;

    public Image[] partnersHp = new Image[3];

    // QTE �� ���� ���� ��ų��ư
    public AllySkillBtn skillBtn;
    // ��ų ��ư �̹��� �迭
    Sprite[] skillIcons = new Sprite[3];

    #region QTE���� �ʵ�
    // qte ���� �ð�
    public float qteTime;
    // qte ���� ����
    public bool isQTESuccess = false;

    // qte ���� ����
    bool isQTEStarted = false;
    // qte Ÿ�̸�
    float qteTimer;
    // qte ������ �ε���
    int qteIdx;
    #endregion

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

        // ���Ḧ ��ȣ�� ���� ������ ������ġ�� ����
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
            qteTimer += Time.deltaTime; // qte Ÿ�̸� ����

            if (qteTimer > qteTime)
            {
                isQTEStarted = false; // qte����
                skillBtn.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
            }

            // QTE�� �����ߴٸ�
            if (isQTESuccess)
            {
                qteTimer = 0f;
                // ������QTE�� �����ٸ�
                if (qteIdx >= partners.Length - 1)
                {
                    isQTEStarted = false; // qte����
                    skillBtn.gameObject.SetActive(false); // ��ư ��Ȱ��ȭ
                    return; // ����������
                }
                else
                {
                    // �ٽ� üũ�ϱ� ���� �ʱ�ȭ
                    isQTESuccess = false;

                    qteIdx++;
                    // ��ų��ư ������ Ȱ��ȭ
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

    // QTE�ߵ� �޼���
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

        // ��ų��ư ������ Ȱ��ȭ
        skillBtn._icon.sprite = skillIcons[qteIdx];
        skillBtn.SetTarget(partners[qteIdx]);
        skillBtn.gameObject.SetActive(true);
    }

}
