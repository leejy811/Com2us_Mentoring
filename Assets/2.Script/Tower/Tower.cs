using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType { Archer = 0, Wizard, Knight, Priest }

public class Tower : MonoBehaviour
{
    public TowerType type;
    public int level { get; private set; }
    public int damage { get; private set; }
    public float attackSpeed { get; private set; }
    public float attackRange { get; private set; }

    protected Transform target;
    protected bool isSpawn;

    public SpriteRenderer radiusSprite;
    public Sprite[] towerSprite;

    //���� ���� ����
    public int buffLevel = 0;
    public int addedDamage = 0;

    private void FixedUpdate()
    {
        if (!isSpawn || type == TowerType.Priest) return;

        target = GetTarget();
    }

    public void Init()
    {
        isSpawn = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<Animator>().enabled = true;

        radiusSprite.gameObject.SetActive(false);
        gameObject.GetComponent<FollowMouse>().enabled = false;

        if (type != TowerType.Priest)
            StartCoroutine("AttackTarget");
    }

    public void SetRangeRadius()
    {
        radiusSprite.transform.localScale = Vector3.one * attackRange * 0.4f;
    }

    protected IEnumerator AttackTarget()
    {
        gameObject.GetComponent<Animator>().SetFloat("AttackSpeed", 1 / attackSpeed);
        while (true)
        {
            if (target == null)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }

            if(type == TowerType.Knight)
            {
                gameObject.GetComponent<Animator>().SetTrigger("doAttack");
                OnAttack();
                yield return new WaitForSeconds(attackSpeed);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetTrigger("doAttack");
                yield return new WaitForSeconds(attackSpeed);
                OnAttack();
            }
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
        transform.localScale = attackDir.x > 0 ? Vector3.one : new Vector3(-1, 1, 1);
    }

    public void PickTower(bool isPick)
    {
        radiusSprite.gameObject.SetActive(isPick);
        SetRangeRadius();

        if (isPick)
        {
            GameManager.instance.uiManager.SetStatWindow(this);
        }
    }
}
