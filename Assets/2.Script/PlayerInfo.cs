using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public int maxHealth;
    public int curHealth { get; private set; }

    public int coin { get; private set; }

    private void Awake()
    {
        curHealth = maxHealth;
        coin = 0;
    }

    public void GetDamage(int damage)
    {
        curHealth -= damage;

        if(curHealth <= 0)
        {
            GameManager.instance.GameOver();
        }
    }

    public void GetCoin(int amount)
    {
        coin += amount;
    }
}
