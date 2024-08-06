public enum PartnerState // AI���� ĳ������ ���µ�
{
    Idle,
    Walk,
    Attack,
    Die,

    Count
}

public enum MonsterState // ������ ���µ�
{
    Idle,
    Walk,
    Attack,
    Skill,
    Die,

    Count
}

public enum AttackType
{
    Short,
    Long
}

public enum SceneType // Scene ����
{
    Unknown,    // ����Ʈ

    Lobby,      // �κ�
    Inventory,  // �κ�
    Stage,      // ��������

    Count      // ����
}

public enum SkillType
{
    Normal,     // �Ϲ� ��ų
    Ultimate,   // �ñر�

    Count
}

public enum BuffType
{
    Speed,
    Defense,
    Heal
}

public enum PartnerType
{
    Defense,
    Attack,
    Support,

    Count
}