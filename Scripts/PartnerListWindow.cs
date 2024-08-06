using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PartnerListWindow : MonoBehaviour
{

    // ������ ������ ��ġ(�θ�)
    public Transform slotTransform;
    // ���� ������
    public GameObject slotPrefab;
    // ������ ĳ������ ������ ������ �ؽ�Ʈ
    public TextMeshProUGUI contextText;

    void Start()
    {
        foreach (var current in GameManager.partnerList)
        {
            Slot_Partner slot = GameObject.Instantiate(slotPrefab, slotTransform).GetComponent<Slot_Partner>();
            slot.SetInfo(current.Value);
        }
    }

    private void Update()
    {
        if (GameManager.Instance.currentInfo != null)
        {
            contextText.text = GameManager.Instance.currentInfo.GetContext();
        }
        else
        {
            contextText.text = "�̸� :\n���� :\n����Ÿ�� :\n��ų :";
        }
    }
    private void OnDisable()
    {
        // �ش� â�� ���ٸ� ���õǾ��ִ� ĭ�� �����Ѵ�.
        GameManager.Instance.currentInfo = null;
    }

    public void BattleStart()
    {
        SceneManager.LoadScene("DefenseScene");
    }

}
