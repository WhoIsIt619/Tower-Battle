using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "unitConfig", menuName = "config/unitConfig")]
public class UnitConfig : ScriptableObject
{
    public UnitType unitType;
    public int health;
    public float moveSpeed;
    public int attack;
    public float detectionRadius;
    public float attackDelay;
    public float attackCooldown;
    public float attackRange;
}

public enum UnitType
{
    Spearman,
    Swordsman,
    Horseman,
}
