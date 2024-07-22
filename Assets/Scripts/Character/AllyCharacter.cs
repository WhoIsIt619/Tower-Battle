using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class AllyCharacter : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public UnitConfig unitConfig;
    public string dieAnimationName;
    public string idleAnimationName;
    public string moveAnimationName;
    public string attackAnimationName;
    public Transform[] waypoints;
    public LayerMask enemyLayer;
    public int currentHealth;
    public float lastAttackTime;

    private int currentWaypointIndex = 0;

    private void Start()
    {
        if (skeletonAnimation == null)
        {
            skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        currentHealth = unitConfig.health;

        StartCoroutine(Move());
    }

    public void PlayAnimation(string animationName, bool loop)
    {
        if (skeletonAnimation != null)
        {
            skeletonAnimation.state.SetAnimation(0, animationName, loop);
        }
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if (Time.time >= lastAttackTime + unitConfig.attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void Die()
    {
        PlayAnimation(dieAnimationName, true);
        Destroy(gameObject);
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, unitConfig.attackRange);
        foreach (var hitCollider in hitColliders)
        {
            EnemyCharacter enemy = hitCollider.GetComponent<EnemyCharacter>();
            if (enemy != null)
            {
                PlayAnimation(attackAnimationName, false);
                enemy.TakeDamage(unitConfig.attack);
            }

            EnemyTower enemyTower = hitCollider.GetComponent<EnemyTower>();
            if (enemyTower != null)
            {
                PlayAnimation(attackAnimationName, false);
                enemyTower.TakeDamage(unitConfig.attack);
            }
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            if (waypoints.Length == 0) yield break;
            int finalWaypointIndex = waypoints.Length - 1;
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            PlayAnimation(moveAnimationName, true);

            while (Vector3.Distance(transform.position, targetWaypoint.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, Time.deltaTime * unitConfig.moveSpeed);
                yield return null;
            }

            PlayAnimation(idleAnimationName, true);
            if (currentWaypointIndex == finalWaypointIndex) yield break;

            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
