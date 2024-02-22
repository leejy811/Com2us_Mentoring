using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] wayPoints;

    [SerializeField] private int maxHealth;
    [SerializeField] private int curHealth;
    [SerializeField] private int damage;
    [SerializeField] private int coin;

    [SerializeField] private Vector3 healthBarOffset;

    private int curIndex = 0;
    private Movement movement;
    private PlayerInfo player;
    private Animator animator;
    private EnemyHealthBar healthBar;

    public void Init(Transform[] points)
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        player = GameManager.instance.player;
        wayPoints = points;
        curHealth = maxHealth;
        curIndex = 0;

        movement.enabled = true;
        transform.position = wayPoints[curIndex].position;

        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        SetHealthBar();
        healthBar.filledAmount = (float)curHealth / (float)maxHealth;
        StartCoroutine("Move");
    }

    private void SetHealthBar()
    {
        GameObject hpBar = GameManager.instance.poolManager.GetPool("HealthBar");
        healthBar = hpBar.GetComponent<EnemyHealthBar>();
        healthBar.GetComponent<EnemyHealthBar>().enemyTransform = transform;
        healthBar.GetComponent<EnemyHealthBar>().offset = healthBarOffset;
    }

    IEnumerator Move()
    {
        NextMove();

        while (true)
        {
            float distance = Vector3.Distance(transform.position, wayPoints[curIndex].position);
            if (distance < 0.05f * movement.moveSpeed)
                NextMove();

            yield return null;
        }
    }

    void NextMove()
    {
        if(curIndex < wayPoints.Length - 1)
        {
            transform.position = wayPoints[curIndex].position;
            curIndex++;
            Vector3 dir = (wayPoints[curIndex].position - transform.position).normalized;
            movement.MoveTo(dir);
        }
        else
        {
            player.GetDamage(damage);
            GameManager.instance.enemySpawner.DieEnemy();
            gameObject.SetActive(false);
        }
    }

    public void GetDamage(int damage)
    {
        if (curHealth - damage <= 0)
        {
            healthBar.filledAmount = 0;
            StartCoroutine(OnDie(0.5f));
            return;
        }
        curHealth -= damage;
        healthBar.filledAmount = (float)curHealth / (float)maxHealth;
    }

    IEnumerator OnDie(float Delay)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("isDie", true);
        player.GetCoin(coin);
        healthBar.gameObject.SetActive(false);
        movement.enabled = false;
        GameManager.instance.enemySpawner.DieEnemy();
        yield return new WaitForSeconds(Delay);
        gameObject.SetActive(false);
    }
}
