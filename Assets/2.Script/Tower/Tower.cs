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
    protected bool isSpawn;

    private void FixedUpdate()
    {
        if (!isSpawn) return;

        target = GetTarget();
    }

    public void Init()
    {
        isSpawn = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<Animator>().enabled = true;

        radiusSprite.gameObject.SetActive(false);
        gameObject.GetComponent<FollowMouse>().enabled = false;
        StartCoroutine("AttackTarget");
    }

    public void SetRangeRadius()
    {
        radiusSprite.transform.localScale = Vector3.one * attackRange * 0.4f;
    }

    protected IEnumerator AttackTarget()
    {
        while (true)
        {
            if (target == null)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }
            OnAttack();
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    protected Transform GetTarget()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Enemy"));
        Transform resultTransform = null;
        float distance = attackRange;
        foreach (Collider2D target in targets)
        {
            Vector3 playerPosition = transform.position;
            Vector3 targetPosition = target.transform.position;
            float curDistance = Vector3.Distance(playerPosition, targetPosition);

            bool checkDistance = curDistance < distance;
            Debug.Log(curDistance);
            if (checkDistance)
            {
                if (curDistance > attackRange)
                    continue;
                distance = curDistance;
                resultTransform = target.transform;
            }
        }
        return resultTransform;
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
        SetRangeRadius();
    }
}
