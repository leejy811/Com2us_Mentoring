using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTower : Tower
{
    [SerializeField] private Vector3 bulletOffset;
    protected override void OnAttack()
    {
        if(target == null) return;

        base.OnAttack();

        string poolname = "";
        switch (type)
        {
            case TowerType.Bow:
                poolname = "BowBullet";
                break;
            case TowerType.Magic:
                poolname = "MagicBullet";
                break;
            default:
                break;
        }

        GameObject bullet = GameManager.instance.poolManager.GetPool(poolname);
        bullet.transform.position = transform.position + bulletOffset * transform.localScale.x;
        bullet.GetComponent<Bullet>().Init(target, damage + addedDamage);
    }
}
