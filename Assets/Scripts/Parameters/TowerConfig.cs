using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerConfig", menuName = "towerConfig/TowerConfig")]
public class TowerConfig : ScriptableObject
{
    public int health;
    public int attackDamage;
    public float attackRange;
    public float attackCooldown;
}
