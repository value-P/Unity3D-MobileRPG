using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCountManager : MonoBehaviour
{
    public GameObject animDoorObj;

    // º¸½º ÇÁ¸®ÆÕ
    public GameObject Boss;

    [SerializeField] List<GameObject> monsters = new List<GameObject>();
    public int monsterCount
    {
        get => monsters.Count;
    }
    public bool monsterDisable
    {
        get
        {
            return monsterCount == 0;
        }
    }

    void Update()
    {
        for(int i = 0; i < monsters.Count; i++)
        {
            var current = monsters[i];
            if (current == null)
            {
                monsters.Remove(current);
                i--;
            }

        }
        if (monsterDisable == true)
        {
            animDoorObj.SetActive(true);
            Boss.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
