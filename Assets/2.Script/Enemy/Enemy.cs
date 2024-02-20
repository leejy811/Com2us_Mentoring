using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] wayPoints;
    public bool isEnemyLive;

    [SerializeField] private int maxHealth;
    [SerializeField] private int curHealth;
    [SerializeField] private int damage;
    [SerializeField] private int coin;

    private int curIndex = 0;
    private Movement movement;
    private PlayerInfo player;
    private Animator animator;

    public void Init(Transform[] points)
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        player = GameManager.instance.player;
        wayPoints = points;
        curHealth = maxHealth;

        transform.position = wayPoints[curIndex].position;

        StartCoroutine("Move");
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
            Destroy(gameObject);
        }
    }

    public void GetDamage(int damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            OnDie();
        }
    }

    void OnDie()
    {
        animator.SetBool("isDie", true);
        player.GetCoin(coin);
        Destroy(gameObject, 0.8f);
    }
}
