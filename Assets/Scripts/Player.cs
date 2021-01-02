using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    int angleOffset = -90;
    Camera mainCamera;
    Vector3 direction;

    CharacterController characterController; 

    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        animator.CrossFade("Player Run", .1f);
        isAttacking = false;
    }

    void Update()
    {
        if (!isAttacking)
        {
            ApplyMovement();
            ApplyRotation();
            Attack();
        }
    }

    void ApplyMovement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        VerifyAnimation(input);
        input = transform.TransformDirection(input);
        input *= moveSpeed;
        characterController.Move(input * Time.deltaTime); 
    }

    void VerifyAnimation(Vector3 input)
    {
        if (input != Vector3.zero)
        {
            animator.SetFloat("MoveSpeed", moveSpeed);
        }
        else 
        {
            animator.SetFloat("MoveSpeed", 0);
        }
    }

    void ApplyRotation()
    {
        if (MouseMoved())
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 playerPosition = mainCamera.WorldToScreenPoint(transform.position);
            direction = mousePosition - playerPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;
            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.up);
        }
    }

    bool MouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackCoroutine());
            List<Enemy> targets;
            if(CheckAttackRange(attackRange,  out targets))
            {
                for(int i=0; i<targets.Count; i++)
                {
                    targets[i].ReceiveDamage(damage);
                }
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
        while (!AttackAnimationFinished()) 
        {
            yield return null;
        }
        animator.SetBool("IsAttacking", false); 
        yield return new WaitForEndOfFrame();
        isAttacking = false;
    }

    public bool CheckAttackRange(float distance, out List<Enemy> hitsResult)
    {
        List<RaycastHit> hits;
        hitsResult = new List<Enemy>();
        bool targetFound = false;
        Vector3 direction = transform.forward;
        GenerateFieldOfView(transform, Quaternion.LookRotation(direction).eulerAngles.y, Vector3.up * .3f, distance, attackRadius, out hits, targetMask, true);
        for(int i=0; i<hits.Count; i++)
        {
            Enemy iDamageable;
            if ((iDamageable = hits[i].collider.GetComponent<Enemy>()) != null && !hitsResult.Contains(hits[i].collider.GetComponent<Enemy>()))
            {
                hitsResult.Add(iDamageable);
                targetFound = true;
            }
        }
        return targetFound;
    }

    protected override void Death()
    {
        base.Death();
        GameManager.instance.GameOver();
    }
}