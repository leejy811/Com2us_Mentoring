using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    int weaponDamage;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public IEnumerator MeleeAttack(int damage, float range)
    {
        weaponDamage = damage;
        boxCollider.size = new Vector2(boxCollider.size.x, range);
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().GetDamage(weaponDamage);
        }
    }
}
