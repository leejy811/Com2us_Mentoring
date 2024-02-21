using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    int weaponDamage;
    BoxCollider2D boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public IEnumerator MeleeAttack(int damage)
    {
        weaponDamage = damage;
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
