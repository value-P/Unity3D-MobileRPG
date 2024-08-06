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

            // ĳ������ ����������ŭ ���� �����Ѵ�.
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

            if (partner.focusTarget != null) // Ÿ���� ���� ��
            {                                    // Ÿ�ٰ��� �Ÿ�
                distance = (partner.focusTarget.transform.position - partner.gameObject.transform.position).magnitude;
                if (distance > partner.AtkRange) // ���ݹ������� �ִٸ� �̵�
                {
                    partner.ChangeState(PartnerState.Walk);
                }
                else                             // ���ݹ������� ������ ����
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
            {   // Ÿ���� ���ٸ� �ٽ� �����·� ���ư���.

                distance = 0;
                partner.ChangeState(PartnerState.Idle);
            }
            else
            {
                if (partner.focusTarget.Stat.CurrentHp < 0) { partner.focusTarget = null; }
                // Ÿ���� ������ �Ÿ� �Ҵ�
                distance = (partner.focusTarget.transform.position - partner.gameObject.transform.position).magnitude;
            }

            partner.anim.SetFloat("isMove", partner.agent.velocity.magnitude);

            if (distance > partner.AtkRange)
            {             // ���ݹ������� ũ�ٸ� �̵�
                partner.agent.SetDestination(partner.focusTarget.transform.position);
            }
            else if (distance <= partner.AtkRange)
            {             // ���ݹ������� �۰ų� ���ٸ� �������� ����
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
            {   // Ÿ���� ���ٸ� �Ҵ�� Ÿ���� ����� �ٽ� �����·� ���ư���.
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
