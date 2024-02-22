using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerSpawner : MonoBehaviour
{
    public bool isPickTower = false;
    public bool isBuyTower = false;
    public GameObject curPickTower;

    List<GameObject> towerList;
    [SerializeField] GameObject[] towerPrefabs;

    void Start()
    {
        towerList = new List<GameObject>();
    }

    public void SpawnTower(Transform spawnPos)
    {
        if (!isBuyTower) return;
            
        towerList.Add(curPickTower);
        curPickTower.transform.position = spawnPos.position;
        curPickTower.GetComponent<Tower>().Init();
        isBuyTower = false;
        curPickTower = null;
    }

    public void BuyTower(int index)
    {
        if (isBuyTower) return;
        isBuyTower = true;

        OffPickAllTower();
        curPickTower = GameManager.instance.poolManager.GetPool(towerPrefabs[index].name);
        curPickTower.GetComponent<Tower>().SetRangeRadius();
    }

    public void ChangeTowerColor(Color color)
    {
        if (!isBuyTower) return;

        curPickTower.GetComponent<Tower>().radiusSprite.color = color;
    }

    public void SetPickTower(bool isPick, GameObject tower)
    {
        isPickTower = isPick;
        curPickTower = tower;
    }

    public void OffPickAllTower()
    {
        foreach(GameObject tower in towerList)
        {
            tower.GetComponent<Tower>().PickTower(false);
        }
    } 
}
