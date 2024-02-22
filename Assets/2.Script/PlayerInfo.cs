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
        coin = 1000;
    }

    public void GetDamage(int damage)
    {
        if (curHealth <= 0) return;
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

    public bool PayCoin(int amount)
    {
        if(coin - amount < 0)
        {
            //µ·³»±â °ÅºÎ
            return false;
        }

        coin -= amount;
        return true;
    }
}
