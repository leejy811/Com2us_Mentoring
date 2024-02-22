using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeTower : Tower
{
    [SerializeField] MeleeWeapon weapon;

    protected override void OnAttack()
    {
        if (target == null) return;

        base.OnAttack();
        StartCoroutine(weapon.MeleeAttack(damage, attackRange));
    }
}
