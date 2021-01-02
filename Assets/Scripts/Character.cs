using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    public LayerMask targetMask;
    protected HitEffect hitEffect;
    protected Animator animator;
    protected bool isAttacking;

    [SerializeField] protected float moveSpeed = 6f;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected float attackRadius = 10f;
    [SerializeField] protected float damage = 10f;

    [SerializeField] float maxHealth = 100f;
    float currentHealth = 100f;
    LifeBar lifeBar;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        hitEffect = GetComponentInChildren<HitEffect>(true);
        lifeBar = CanvasManager.instance.CreateLifeBar(this);
    }

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
        lifeBar.gameObject.SetActive(true); 
        lifeBar.SetAmount(GetHealthPercentaje());
    }

    protected virtual void OnDisable()
    {
        if(lifeBar != null)
        {
            lifeBar.gameObject.SetActive(false);
        }
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }

    public void ReceiveDamage(float damage)
    {
        hitEffect.gameObject.SetActive(true);
        if (currentHealth - damage <= 0)
        {
            currentHealth = 0;
            Death();
        }
        else
        {
            currentHealth -= damage;
        }
        lifeBar.SetAmount(GetHealthPercentaje());
    }

    public float GetHealthPercentaje()
    {
        return currentHealth / maxHealth;
    }

    public void GenerateFieldOfView(Transform transform, float direction, Vector3 offset, float rayLenght, float viewAngle, out List<RaycastHit> hits, LayerMask layerMask, bool debug = false)
    {
        int rayCount = (int)(viewAngle * 2 / 5);
        hits = new List<RaycastHit>();
        float anglePerLoop = viewAngle * 2 / rayCount;
        Vector3 currentDir;
        Vector3 origin = transform.position + offset;
        for (int i = 0; i < rayCount; i++)
        {
            RaycastHit hit;
            currentDir = DirFromAngle(-viewAngle + anglePerLoop * i + direction);
            bool ray = Physics.Raycast(origin, currentDir, out hit, rayLenght, layerMask);
            if (ray)
                hits.Add(hit);
            if (debug)
            {
                if (ray)
                    Debug.DrawRay(origin, currentDir * hit.distance, Color.yellow);
                else
                    Debug.DrawRay(origin, currentDir * rayLenght, Color.blue);
            }
        }
    }

    public static Vector3 DirFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    protected bool AttackAnimationFinished()
    {
        return Mathf.Clamp01(animator.GetCurrentAnimatorStateInfo(0).normalizedTime) == 1;
    }


    protected virtual void Death()
    {
        gameObject.SetActive(false);
    }
}