using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    ProjectileBase from = null; 

    void Start()
    {
        if (from == null)
        {       
                from = GetComponentInParent<ProjectileBase>();
        }
        
    }

    public void OnTriggerEnter(Collider collision) { from?.OnTriggerEnter(collision); }
}
