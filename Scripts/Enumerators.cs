public enum PartnerState 
{
    Idle,
    Walk,
    Attack,
    Die,

    Count
}

public enum MonsterState
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

public enum SceneType 
{
    Unknown,   

    Lobby,     
    Inventory, 
    Stage,     

    Count      
}

public enum SkillType
{
    Normal,    
    Ultimate,  

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