using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // 주인
    public MovableBase owner { get; protected set; }

    // 목표물
    public MovableBase focusTarget;

    // 충돌이벤트들
    protected ProjectileAction[] actions;

    // 충돌무시 리스트
    protected List<GameObject> ignoreList = new List<GameObject>();

    // 방향
    protected Vector3 _direction;
    public Vector3 Direction
    {
        get => _direction;
        set
        {
            _direction = value.normalized;
        }
    }

    // �߻�ü�� Ư�� ��ġ
    #region
    [Tooltip("발사체의 특성")]
    public float currentSpeed;
    //[Tooltip("�߻�ü�� ���ӵ� - �ð��� ���� ���������� ��ȭ�ϴ� �ӵ���")]
    //public float acceleration;
    //[Tooltip("�߻�ü�� ���ӵ� - �ð��� ������ ȸ��")]
    //public float angularSpeed;
    [Tooltip("해당 발사체의 생존 시간")]
    public float leftTime;
    [Tooltip("주인과 충돌이 가능한가?")]
    public bool contactSelf;
    [Tooltip("타겟을 계속 추적할 것인가?")]
    public bool isTracking;
    [Tooltip("�������ΰ�?")]
    public bool isRangeAttack;
    [Tooltip("버프인가?")]
    public bool isBuff;
    #endregion

    void Start()
    {
        foreach (Collider current in GetComponentsInChildren<Collider>())
        {
            // �ݶ��̴��� �ִ� ������Ʈ���� �浹���� ����� �Ҵ��Ų��.
            current.gameObject.AddComponent<ProjectileCollider>();
        }

        // �߻�ü�� �ൿ���� ��Ƽ� �Ҵ���ѵα�
        actions = GetComponents<ProjectileAction>();
    }

    void Update()
    {
        if (leftTime <= 0) { Destroy(gameObject); }
        leftTime -= Time.deltaTime; // �߻�ü�� �����ð�

        // �ð��� �������� ����
        // currentSpeed += acceleration * Time.deltaTime;

        if (isTracking && !isBuff)
        {
            // 추적하는 스킬이라면, 타겟이 없을 때 사라지게 하고 있다면 타겟을 바라보고 계속 전진하게 한다.
            if (focusTarget == null) { Destroy(gameObject); }

            if (focusTarget)
            {
                Vector3 targetPosition = focusTarget.transform.position;
                targetPosition.y += 0.5f;
                transform.LookAt(targetPosition);
                transform.position += transform.forward * currentSpeed * Time.deltaTime;
            }
        }
        else if (!isTracking && !isBuff)
        {
            // 추적하는 스킬이 아니라면, 스피드가 있을 때 초기화 부분에서 받은 방향으로 바라보게 만든다.
            // 스피드가 없다면 방향조절을 하지 않을거기 때문에 나누었다.
            if (currentSpeed > 0)
            {
                if (focusTarget == null) { Destroy(gameObject); }
                Vector3 lookPosition = new Vector3(Direction.x, 0, Direction.z).normalized;
                transform.LookAt(transform.position + lookPosition);
            }


            Vector3 movePosition = Direction * currentSpeed * Time.deltaTime;
            transform.position += movePosition;
        }
        else if (isBuff && focusTarget != null)
        {
            Vector3 position = focusTarget.transform.position;
            position.y += 0.5f;
            transform.position = position;
        }
    }

    public void Initialize(MovableBase wantOwner, MovableBase wantTarget, bool wantTracking)
    {
        // 소환되면서 초기화
        owner = wantOwner;
        isTracking = wantTracking;
        if (!contactSelf && owner != null) { SetIgnore(owner.gameObject); }

        if (wantTarget != null)
        {
            focusTarget = wantTarget;
            Direction = focusTarget.transform.position - owner.transform.position;
        }
    }

    public void Initialize(MovableBase wantOwner)
    {
        owner = wantOwner;
        isTracking = false;
        if (!contactSelf && owner != null) { SetIgnore(owner.gameObject); }

        focusTarget = null;
        Direction = owner.transform.forward;
    }

    public void BuffInitialize(MovableBase wantOwner, MovableBase wantTarget, bool wantBuff)
    {
        owner = wantOwner;

        if (wantTarget != null)
        {
            focusTarget = wantTarget;
            transform.position = wantTarget.transform.position;
        }
        isBuff = wantBuff;
    }

    public virtual void Activate(MovableBase other)
    {
        // 예외처리1.충돌을 무시하기로한 물체라면 return
        if (ignoreList.Contains(other.gameObject)) { return; }
        // 예외처리2.같은 진영이라면 return
        if (owner.isAlly == other.isAlly && !isBuff) { return; }

        if (isTracking) // 현재 스킬이 추적 스킬이라면
        {
            // 타겟이 아니면 return
            if (owner.focusTarget != other) { return; }
            // 타겟이면 타겟 대상으로 액션 실행
            else { ActionActivate(this, focusTarget, transform.position); }
        }
        else
        {
            if (owner.isAlly == other.isAlly && isBuff)
            {
                ActionActivate(this, other, transform.position);
            }

            if (owner.isAlly != other.isAlly)
            {
                ActionActivate(this, other, transform.position);
            }
        }
    }

    void ActionActivate(ProjectileBase wantProj, MovableBase wantTarget, Vector3 wantPosition)
    {
        foreach (ProjectileAction current in actions) { current?.Activate(wantProj, wantTarget, wantPosition); }
    }

    public void OnTriggerEnter(Collider other)
    {
        // 예외처리.캐릭터가 아니라면 return
        if (other.GetComponent<MovableBase>() == null) { return; }
        // 캐릭터 일때만 충돌 이벤트 함수 실행하게 설정
        else
        {
            Activate(other.GetComponent<MovableBase>());
        }
    }

    public virtual void SetIgnore(GameObject target)
    {
        ignoreList.Add(target); //����� �浹�����Ѵ�.(Layer�� Ȱ���ؼ� �浹�� ������ �� �Լ� ���ʿ�)
    }
}
