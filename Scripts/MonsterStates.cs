using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterStates
{
    public class MIdle : MState
    {
        public override void OnEnter(MonsterBase monster)
        {
            monster.anim.SetBool("isBattle", false);
            monster.focusTarget = null;
            monster.hpUI.SetActive(false);
        }

        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.focusTarget == null)
            {
                float nearestDistance = 10f;
                int nearestIndex = -1;

                // ĳ������ ����������ŭ ���� �����Ѵ�.
                Collider[] col = Physics.OverlapSphere(monster.gameObject.transform.position, monster.FindRange, 1 << 9 | 1 << 10);
                for (int i = 0; i < col.Length; i++)
                {
                    float tempDistance = (col[i].gameObject.transform.position - monster.gameObject.transform.position).magnitude;
                    if (tempDistance < nearestDistance)
                    {
                        nearestDistance = tempDistance;
                        nearestIndex = i;
                    }
                }

                if (nearestIndex > -1)
                {
                    monster.focusTarget = col[nearestIndex].GetComponent<MovableBase>();
                }
                else { monster.focusTarget = null; }

            }

            if (monster.focusTarget != null)
            {
                distance = (monster.focusTarget.transform.position - monster.gameObject.transform.position).magnitude;
                monster.findEffect.SetActive(true);

                if (distance > monster.AtkRange) // ���ݹ������� �ִٸ� �̵�
                {
                    monster.ChangeState(MonsterState.Walk);
                }
                else                             // ���ݹ������� ������ ����
                {
                    monster.ChangeState(MonsterState.Attack);
                }
            }
        }

        public override void OnExit(MonsterBase monster)
        {
            monster.hpUI.SetActive(true);
        }
    }

    public class MMove : MState
    {
        float moveTime = 0f;
        public override void OnEnter(MonsterBase monster)
        {
            monster.agent.enabled = true;
            moveTime = 0f;
        }


        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.focusTarget == null)
            {
                distance = 0;
                monster.ChangeState(MonsterState.Idle);
            }
            else
            {
                if (monster.focusTarget.Stat.CurrentHp < 0) { monster.focusTarget = null; }
                distance = (monster.focusTarget.transform.position - monster.gameObject.transform.position).magnitude;
            }

            monster.anim.SetFloat("isMove", monster.agent.velocity.magnitude);

            if (distance > monster.AtkRange)
            {             // ���ݹ������� ũ�ٸ� �̵�
                monster.agent.SetDestination(monster.focusTarget.transform.position);
                moveTime += Time.deltaTime;
                if (moveTime >= 3.5f)
                {
                    monster.focusTarget = null;
                    monster.ChangeState(MonsterState.Idle);
                }
            }
            else if (distance <= monster.AtkRange)
            {             // ���ݹ������� �۰ų� ���ٸ� �������� ����
                monster.ChangeState(MonsterState.Attack);
                if (moveTime > 0) { moveTime = 0f; }
            }
        }
        public override void OnExit(MonsterBase monster)
        {
            monster.agent.enabled = false;
        }
    }

    public class MAttack : MState
    {
        public override void OnEnter(MonsterBase monster)
        {
            monster.Stat.AttackSpeed = 0f;
            if (monster.focusTarget != null)
            {
                monster.transform.LookAt(monster.focusTarget.transform);
            }
        }


        public override void OnUpdate(MonsterBase monster)
        {
            if (monster.focusTarget == null)
            {   // Ÿ���� ���ٸ� �Ҵ�� Ÿ���� ����� �ٽ� �����·� ���ư���.
                distance = 0;
                monster.ChangeState(MonsterState.Idle);
            }
            else if (monster.focusTarget != null && monster.focusTarget.Stat.CurrentHp <= 0)
            {
                monster.focusTarget = null;
            }
            else if (monster.focusTarget.Stat.CurrentHp > 0)
            {
                distance = (monster.focusTarget.transform.position - monster.gameObject.transform.position).magnitude;
                monster.transform.LookAt(monster.focusTarget.transform);
            }


            if (monster.focusTarget != null && distance > monster.AtkRange)
            {
                monster.ChangeState(MonsterState.Walk);
            }
            else if (monster.focusTarget != null && distance <= monster.AtkRange && monster.Stat.AttackSpeed <= 0)
            {
                monster.anim.SetFloat("isMove", monster.agent.velocity.magnitude);
                monster.anim.SetBool("isBattle", true);
            }

            if (monster.Stat.AttackSpeed > 0)
            {
                monster.anim.SetFloat("isMove", monster.agent.velocity.magnitude);
                monster.anim.SetBool("isBattle", false);
                monster.Stat.AttackSpeed -= Time.deltaTime;
            }

            if (monster.equipSkill != null && monster.Stat.HPRate < 0.7f && monster.hitStack == 0)
            {
                monster.SkillCasting();
                monster.hitStack++;
            }
            if (monster.equipSkill != null && monster.Stat.HPRate < 0.3f && monster.hitStack == 1)
            {
                monster.SkillCasting();
                monster.hitStack++;
            }
        }
        public override void OnExit(MonsterBase monster)
        {
            monster.anim.SetBool("isBattle", false);
        }
    }

    public class MDie : MState
    {
        public override void OnEnter(MonsterBase monster)
        {
            monster.anim.SetTrigger("isDie");
        }
        public override void OnUpdate(MonsterBase monster)
        {
        }

        public override void OnExit(MonsterBase monster)
        {
        }

    }
}