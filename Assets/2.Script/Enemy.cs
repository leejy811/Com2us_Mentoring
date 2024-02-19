using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] wayPoints;

    private int curIndex = 0;
    private Movement movement;

    public void Init(Transform[] points)
    {
        movement = GetComponent<Movement>();
        wayPoints = points;

        transform.position = wayPoints[curIndex].position;

        StartCoroutine("Move");
    }

    IEnumerator Move()
    {
        NextMove();

        while (true)
        {
            float distance = Vector3.Distance(transform.position, wayPoints[curIndex].position);
            if (distance < 0.02f * movement.moveSpeed)
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
            Destroy(gameObject);
        }
    }
}
