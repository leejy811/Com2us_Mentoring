using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;

    protected void FixedUpdate()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector3 attckDir = (target.position - transform.position).normalized;
        transform.position += attckDir * speed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, attckDir);
    }

    public void Init(Transform tar, int damage)
    {
        this.damage = damage;
        target = tar;
        Vector3 attckDir = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, attckDir);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
