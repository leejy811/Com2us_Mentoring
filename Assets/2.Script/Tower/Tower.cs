using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected int level;
    [SerializeField] protected int damage;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackRange;
    
    protected Transform target;

    [SerializeField] protected SpriteRenderer radiusSprite;
    protected bool isPick;

    private void Start()
    {
        StartCoroutine("AttackTarget");
    }

    protected IEnumerator AttackTarget()
    {
        while (true)
        {
            GetTarget();
            OnAttack();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    protected void GetTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        float distance = attackRange;
        foreach (Collider2D target in targets)
        {
            Vector3 playerPosition = transform.position;
            Vector3 targetPosition = target.transform.position;
            float curDistance = Vector3.Distance(playerPosition, targetPosition);

            bool checkDistance = curDistance < distance;

            if (checkDistance)
            {
                if (curDistance > attackRange)
                    continue;
                distance = curDistance;
                this.target = target.transform;
            }
        }
    }

    protected virtual void OnAttack()
    {
        Vector3 attackDir = target.position - transform.position;
        transform.localScale = attackDir.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    public void PickTower()
    {
        isPick = !isPick;
        radiusSprite.gameObject.SetActive(isPick);
        radiusSprite.transform.localScale = Vector3.one * attackRange;
    }
}
