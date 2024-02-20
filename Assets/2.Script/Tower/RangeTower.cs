using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTower : Tower
{
    [SerializeField] private Vector3 bulletOffset;
    [SerializeField] private GameObject bulletPrefab;
    protected override void OnAttack()
    {
        if(target == null) return;

        base.OnAttack();
        GameObject bullet = Instantiate(bulletPrefab, transform.position + bulletOffset, Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(target, damage + addedDamage);
    }
}
