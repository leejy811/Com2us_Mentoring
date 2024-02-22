using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public List<int>[] towerEnhancePrice = new List<int>[4];
    public List<int>[] towerSellPrice = new List<int>[4];
    public List<int>[] towerSuccessProb = new List<int>[4];
    public List<int>[] towerDestroyProb = new List<int>[4];

    private void Awake()
    {
        for(int i = 0; i < towerEnhancePrice.Length; i++)
        {
            towerEnhancePrice[i] = new List<int>();
            towerSellPrice[i] = new List<int>();
            towerSuccessProb[i] = new List<int>();
            towerDestroyProb[i] = new List<int>();
        }
    }
}
