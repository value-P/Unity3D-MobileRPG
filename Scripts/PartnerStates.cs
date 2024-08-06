using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PartnerStates
{

    public class PIdle : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            partner.anim.SetBool("isBattle", false);

        }

        public override void OnUpdate(PartnerBase partner)
        {
            float nearestDistance = 10f;
            int nearestIndex = -1;
            if (partner.focusTarget == null)
            {
                Vector3 position = partner.idlePosition.position;
                position.y = partner.gameObject.transform.position.y;
                float playerDistance = (position - partner.gameObject.transform.position).magnitude;

                if (playerDistance > 1.5f)
                {
                    partner.agent.enabled = true;
                    partner.agent.SetDestination(GameManager.Instance.player.gameObject.transform.position);
                    partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);
                }
                else
                {
                    partner.agent.enabled = false;
                    partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);
                }
            }
            else
            {
                partner.agent.enabled = false;
                partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);
            }

            // 캐릭터의 감지범위만큼 적을 감지한다.
            Collider[] col = Physics.OverlapSphere(partner.gameObject.transform.position, partner.FindRange, 1 << 11);
            for (int i = 0; i < col.Length; i++)
            {
                float tempDistance = (col[i].gameObject.transform.position - partner.gameObject.transform.position).magnitude;
                if (tempDistance < nearestDistance)
                {
                    nearestDistance = tempDistance;
                    nearestIndex = i;
                }
            }

            if (nearestIndex > -1)
            {
                partner.focusTarget = col[nearestIndex].GetComponent<MovableBase>();

            }
            else { partner.focusTarget = null; }

            if (partner.focusTarget != null) // 타겟이 있을 때
            {                                    // 타겟과의 거리
                distance = (partner.focusTarget.transform.position - partner.gameObject.transform.position).magnitude;
                if (distance > partner.AtkRange) // 공격범위보다 멀다면 이동
                {
                    partner.ChangeState(PartnerState.Walk);
                }
                else                             // 공격범위보다 가까우면 공격
                {
                    partner.ChangeState(PartnerState.Attack);
                }
            }
        }

        public override void OnExit(PartnerBase partner)
        {
        }
    }


    public class PMove : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            partner.agent.enabled = true;
        }

        public override void OnUpdate(PartnerBase partner)
        {
            if (partner.focusTarget == null)
            {   // 타겟이 없다면 다시 대기상태로 돌아간다.

                distance = 0;
                partner.ChangeState(PartnerState.Idle);
            }
            else
            {
                if (partner.focusTarget.Stat.CurrentHp < 0) { partner.focusTarget = null; }
                // 타겟이 있으면 거리 할당
                distance = (partner.focusTarget.transform.position - partner.gameObject.transform.position).magnitude;
            }

            partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);

            if (distance > partner.AtkRange)
            {             // 공격범위보다 크다면 이동
                partner.agent.SetDestination(partner.focusTarget.transform.position);
            }
            else if (distance <= partner.AtkRange)
            {             // 공격범위보다 작거나 같다면 공격으로 변경
                partner.ChangeState(PartnerState.Attack);
            }
        }

        public override void OnExit(PartnerBase partner)
        {
            partner.agent.enabled = false;
        }
    }

    public class PAttack : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            partner.Stat.AttackSpeed = 0f;
        }

        public override void OnUpdate(PartnerBase partner)
        {
            if (partner.focusTarget == null)
            {   // 타겟이 없다면 할당된 타겟을 지우고 다시 대기상태로 돌아간다.
                distance = 0;
                partner.ChangeState(PartnerState.Idle);
            }
            else if (partner.focusTarget != null && partner.focusTarget.Stat.CurrentHp <= 0)
            {
                partner.focusTarget = null;
            }
            else if (partner.focusTarget.Stat.CurrentHp > 0)
            {
                distance = (partner.focusTarget.transform.position - partner.gameObject.transform.position).magnitude;
                partner.transform.LookAt(partner.focusTarget.transform);
            }

            if (partner.focusTarget != null && distance > partner.AtkRange)
            {
                partner.ChangeState(PartnerState.Walk);
            }
            else if (partner.focusTarget != null && distance <= partner.AtkRange && partner.Stat.AttackSpeed <= 0)
            {
                partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);
                partner.anim.SetBool("isBattle", true);
            }

            if (partner.Stat.AttackSpeed > 0)
            {
                partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);
                partner.anim.SetBool("isBattle", false);
                partner.Stat.AttackSpeed -= Time.deltaTime;
            }
        }

        public override void OnExit(PartnerBase partner)
        {
            partner.anim.SetBool("isBattle", false);
        }
    }

    public class PDie : PState
    {
        public override void OnEnter(PartnerBase partner)
        {
            partner.anim.SetTrigger("isDie");
        }


        public override void OnUpdate(PartnerBase partner)
        {
        }
        public override void OnExit(PartnerBase partner)
        {
        }
    }
}
