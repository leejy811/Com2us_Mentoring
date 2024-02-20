using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 moveDir = Vector3.zero;

    private void Update()
    {
        transform.position += moveSpeed * moveDir * Time.deltaTime;
    }

    public void MoveTo(Vector3 dir)
    {
        transform.localScale = dir.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        moveDir = dir;
    }
}
