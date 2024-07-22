using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EnemyTowerConfig", menuName = "towerConfig/EnemyTowerConfig")]
public class EnemyTowerConfig : ScriptableObject
{
    public int health;
    public int attackDamage;
    public float attackRange;
    public float attackCooldown;
}
