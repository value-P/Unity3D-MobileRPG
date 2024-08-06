using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    // �߻�ü�� ����
    ProjectileBase from = null; 

    void Start()
    {
        if (from == null)
        {       // �θ� ������Ʈ�� �ִ� ���� �Ҵ����ֱ�
                from = GetComponentInParent<ProjectileBase>();
        }
        
    }

    public void OnTriggerEnter(Collider collision) { from?.OnTriggerEnter(collision); }

    //public void OnCollisionEnter(Collision collision) { from?.OnCollisionEnter(collision); }
}
