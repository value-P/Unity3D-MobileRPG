using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float leftTime;
    void Start()
    {
        Destroy(gameObject, leftTime);
    }
}
