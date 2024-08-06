using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovableNPC : MonoBehaviour
{
    [SerializeField] Transform[] wayPoints; // 캐릭터가 움직일 목적지
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
    {//       목적지와 오브젝트의 거리를 측정해 근접하면 Idle, 아니라면 Move
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
            if (wayCount >= wayPoints.Length) { wayCount = 0; } // 목적지 번호가 배열의 크기보다 크다면 번호 초기화

            agent.SetDestination(wayPoints[wayCount].position);
        }
    }
}
