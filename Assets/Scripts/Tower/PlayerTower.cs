using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class PlayerTower : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public TowerConfig config;
    public string dieAnimationName;
    public string idleAnimationName;
    public string attackAnimationName;
    private int currentHealth;
    private float lastAttackTime;
    public Image allyHealthBar;
    public GameManager gameManager;

    void Start()
    {
        PlayAnimation(idleAnimationName, true);
        currentHealth = config.health;
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

        if (Time.time >= lastAttackTime + config.attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        allyHealthBar.fillAmount = (float)currentHealth / config.health;
    }

    void Die()
    {
        PlayAnimation(dieAnimationName, false);
        Destroy(gameObject);
        gameManager.LoadDefeatMenu();
    }

    void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, config.attackRange);
        foreach (var hitCollider in hitColliders)
        {
            EnemyCharacter enemy = hitCollider.GetComponent<EnemyCharacter>();
            if (enemy != null)
            {
                PlayAnimation(attackAnimationName, false);
                enemy.TakeDamage(config.attackDamage);
            }
        }
    }
}
