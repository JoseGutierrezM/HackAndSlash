using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [HideInInspector] public NavMeshAgent agent;

    public Action<Enemy> onDeath; 
    Character target;

    [SerializeField] float attackCooldown = 10;
    float attackTimer;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        animator.SetFloat("MoveSpeed", moveSpeed);
        animator.SetBool("IsAttacking", false);
        agent.isStopped = false;
        isAttacking = false;
    }


    void Update()
    {
        if (!isAttacking)
        {
            Chase();
        }
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown) 
        {
            Attack();
            attackTimer = 0;
        }
    }

    public void AssignTarget(Character _target)
    {
        target = _target;
    }

    void Chase()
    {
        if (target != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }

    void Attack()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < attackRange)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        agent.isStopped = true;
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("IsAttacking", false);
        yield return new WaitForEndOfFrame();
        isAttacking = false;
        agent.isStopped = false;
        Melee(transform.position, damage);
    }

    public void Melee(Vector3 position, float damage)
    {
        Collider[] colliders = Physics.OverlapSphere(position, attackRange, targetMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Player>() != null)
            {
                colliders[i].GetComponent<Player>().ReceiveDamage(damage);
                break;
            }
        }
    }

    protected override void Death()
    {
        base.Death();
        onDeath?.Invoke(this);
    }
}