using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeableCharacterBase : MovableBase
{
    float moveAttackSpeed = 0.2f;

    [HideInInspector] public Vector3 inputVector; // 이동입력값

    Rigidbody rigid;

    #region 상태Bool값
    public bool isRun = false; // 달리기 상태
    public bool isDead = false; // 죽었는가
    protected bool isAttacking = false; // 공격중인가
    #endregion

    #region 스킬이름 & WeaponBase
    [SerializeField] protected string ultimateWeaponName; // 궁극기 이름
    [HideInInspector] public WeaponBase ultimateSkill; // 궁극기
    #endregion

    #region 스킬 쿨타임
    [SerializeField] protected float ultimateSkillCooldown; // 궁극기 쿨타임
    #endregion

    #region 시야 관련
    [SerializeField] protected float viewAngle = 130f; // 시야각
    [SerializeField] protected float viewDistance = 10f; // 시야거리
    [SerializeField] protected LayerMask targetMask; // 몬스터 레이어
    #endregion

    public Transform[] partnerPos;

    // 허용최대 경사각
    public float maxSlopeAngle;

    protected override void Start()
    {
        base.Start();
        rigid = GetComponent<Rigidbody>();

        ultimateSkill = AddWeapon(ultimateWeaponName, ultimateSkillCooldown);
        meshs = GetComponentsInChildren<SkinnedMeshRenderer>();
        // 궁극기 쿨타임 사이클 추가
        MovableUpdate += UltimateCoolTimeCycle;
    }

    protected override void Update()
    {
        base.Update();

        if (stat.CurrentHp <= 0 && !isDead) Die(); // 사망

        View(); // 시야각 내의 가장 가까운적을 타겟으로 설정

        Move(inputVector); // 입력받은 값에 따라 움직임
    }

    public virtual void Move(Vector3 inputDir)
    {
        anim.SetFloat("MoveSpeed", inputDir.magnitude);

        // �Է� ������ ������ �������� �ƴҶ�
        if (inputDir.magnitude > 0.1f && !isAttacking)
        {
            float dirX = inputDir.x;
            float dirZ = inputDir.y;
            Vector3 moveDir = new Vector3(dirX, 0, dirZ);

            transform.position += (moveDir * stat.MoveSpeed * Time.deltaTime) * (isRun ? stat.moveSpeedMultiflier : 1f);

            anim.SetBool("IsRun", isRun);

            Vector3 lookPos = new Vector3(moveDir.x, 0, moveDir.z);

            transform.LookAt(transform.position + lookPos);
        }
    }

    public virtual void Attack(bool onAttack)
    {
        anim.SetBool("OnAttack", onAttack);
        isAttacking = onAttack;
    }

    public virtual void Skill(SkillType skillType) // 타입에 따른 스킬발동
    {

        switch (skillType)
        {
            case SkillType.Normal:
                {
                    if (equipSkill.CurrentCoolTime <= 0)
                    {
                        equipSkill.CurrentCoolTime = equipSkill.CoolTime;
                        equipSkill.OnAttack(focusTarget, false);
                        anim.SetTrigger("OnSkill");
                    }
                }
                break;
            case SkillType.Ultimate:
                {
                    if (ultimateSkill.CurrentCoolTime <= 0)
                    {
                        ultimateSkill.CurrentCoolTime = ultimateSkill.CoolTime;
                        ultimateSkill.OnAttack(focusTarget, false);
                        anim.SetTrigger("OnSkill");
                    }
                }
                break;
        }
    }

    public override float GetDamage(float damage, MovableBase from)
    {
        stat.CurrentHp -= damage;

        return damage;
    }

    protected void Die() // 체력이 0이 되면 사망
    {
        isDead = true;
        anim.SetTrigger("OnDead");
    }

    // 가장 가까운 적 타겟으로
    protected void View()
    {
        float nearesttargetDist = viewDistance + 1;
        int nearesttargetIdx = -1;

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            Vector3 _direction = (_targetTf.position - transform.position).normalized;
            // 시야각을 통해 검사
            float _angle = Vector3.Angle(_direction, transform.forward);
            if (_angle < viewAngle * 0.5f)
            {

                float dist = (_targetTf.position - transform.position).magnitude;
                if (dist < nearesttargetDist)
                {
                    nearesttargetDist = dist;
                    nearesttargetIdx = i;
                }
            }
        }

        if (nearesttargetIdx > -1)
            focusTarget = _target[nearesttargetIdx].GetComponent<MovableBase>();
        else
            focusTarget = null;

    }

    public override WeaponBase AddWeapon(string wantName, float coolTime)
    {
        switch (wantName)
        {
            case "FloatingSword":
                return new Weapon_FloatingSword(this, coolTime);
            case "SwordStorm":
                return new Weapon_SwordStorm(this, coolTime);
        }

        return null;
    }

    public virtual void ActiveAttackCol()
    {
        atkCollider.enabled = true;
    }

    public virtual void InActiveAttackCol()
    {
        atkCollider.enabled = false;
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    void UltimateCoolTimeCycle()
    {
        if (ultimateSkill != null && ultimateSkill.CurrentCoolTime > 0)
        {
            ultimateSkill.CurrentCoolTime -= Time.deltaTime;
        }
    }

}
