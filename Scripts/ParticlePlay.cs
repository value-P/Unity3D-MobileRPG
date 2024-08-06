using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour
{
    ParticleSystem particle;
    public float leftTime;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        particle?.Play();
        Invoke("ActiveFalse", leftTime);
    }
    private void OnDisable()
    {
        particle?.Stop();
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
