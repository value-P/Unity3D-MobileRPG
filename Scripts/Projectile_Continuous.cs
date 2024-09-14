using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Continuous : ProjectileAction
{
    [SerializeField] float attackDelay = 0.3f;
    float checkTime = 0f;
    Collider col;

    private void Start()
    {
        col = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        checkTime += Time.deltaTime;
        if(checkTime >= attackDelay)
        {
            checkTime = 0f;
            StartCoroutine(ActiveCollier());
        }
    }

    IEnumerator ActiveCollier()
    {
        col.enabled = true;
        yield return new WaitForFixedUpdate();
        col.enabled = false;
    }
}
