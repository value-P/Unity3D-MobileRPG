using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBase : MonoBehaviour
{
    [SerializeField] protected float _currentHp;         
    [SerializeField] protected float _maxHp;             
    [SerializeField] protected float _moveSpeed;         
    [SerializeField] protected float _attackSpeed;       
    [SerializeField] protected float _attackDelay;       
    [SerializeField] protected float _resistance;        
    [SerializeField] protected float _attackPower;       
    [SerializeField] protected float _defensePower;      
    [SerializeField] protected float _selfRecovery;      

    public float moveSpeedMultiflier = 2f;

    public float CurrentHp
    {
        get => _currentHp;
        set => _currentHp = Mathf.Clamp(value, 0, _maxHp);
    }
    public float MaxHP { get => _maxHp; }
    public float HPRate { get => _currentHp / _maxHp; } 
    public float MoveSpeed { get => _moveSpeed; }
    public float AttackSpeed
    {
        get => _attackSpeed;
        set
        {
            _attackSpeed = value;
        }
    }
    public float AttackDelay
    {
        get => _attackDelay;
        set
        {
            _attackDelay = value;
        }
    }
    public float Resistance { get => _resistance; }
    public float AttackPower { get => _attackPower; }
    public float DefensePower
    {
        get => _defensePower;
        set
        {
            _defensePower = value;
        }
    }
    public float SelfRecovery { get => _selfRecovery; }

}
