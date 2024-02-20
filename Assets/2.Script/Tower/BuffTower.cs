using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTower : Tower
{
    private void FixedUpdate()
    {
        OnBuff();
    }

    void OnBuff()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Tower"));

        foreach (Collider2D target in targets)
        {
            if (target.gameObject.GetComponent<BuffTower>() != null) continue;

            Tower tower = target.gameObject.GetComponent<Tower>();
            if (buffLevel < level)
            {
                tower.buffLevel = level;
                tower.addedDamage = damage;
            }
        }
    }

    void OffBuff()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Tower"));

        foreach (Collider2D target in targets)
        {
            if (target.gameObject.GetComponent<BuffTower>() != null) continue;

            Tower tower = target.gameObject.GetComponent<Tower>();
            tower.buffLevel = 0;
            tower.addedDamage = 0;
        }
    }
}
