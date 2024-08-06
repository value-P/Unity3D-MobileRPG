using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovableNPC : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints; // ĳ���Ͱ� ������ ������
    int wayCount = 0;
    NavMeshAgent agent;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        InvokeRepeating("MoveToNextWayPoint", 0f, 3f);
    }

    void Update()
    {//       �������� ������Ʈ�� �Ÿ��� ������ �����ϸ� Idle, �ƴ϶�� Move
        if ((wayPoints[wayCount].position- gameObject.transform.position).magnitude <=0.2f)
        {
            anim.SetBool("isMove", false);
        }
        else
        {
            anim.SetBool("isMove", true);
        }
    }

    void MoveToNextWayPoint()
    {
        if (agent.velocity == Vector3.zero)
        {
            wayCount++;
            if (wayCount >= wayPoints.Length) { wayCount = 0; } // ������ ��ȣ�� �迭�� ũ�⺸�� ũ�ٸ� ��ȣ �ʱ�ȭ

            agent.SetDestination(wayPoints[wayCount].position);
        }
    }
}
