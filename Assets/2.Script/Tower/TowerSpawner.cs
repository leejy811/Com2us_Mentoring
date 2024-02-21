using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerSpawner : MonoBehaviour
{
    public bool isPickTower = false;

    GameObject curPickTower;
    List<GameObject> towerList;
    [SerializeField] GameObject[] towerPrefabs;

    void Start()
    {
        towerList = new List<GameObject>();
    }

    public void SpawnTower(Transform spawnPos)
    {
        if (!isPickTower) return;
        towerList.Add(curPickTower);
        curPickTower.transform.position = spawnPos.position;
        curPickTower.GetComponent<Tower>().Init();
        isPickTower = false;
        curPickTower = null;
    }

    public void ChangePickState(int index)
    {
        if (isPickTower) return;
        isPickTower = true;

        OffPickAllTower();
        curPickTower = GameManager.instance.poolManager.GetPool(towerPrefabs[index].name);
        curPickTower.GetComponent<Tower>().SetRangeRadius();
    }

    public void ChangeTowerColor(Color color)
    {
        if (!isPickTower) return;

        curPickTower.GetComponent<Tower>().radiusSprite.color = color;
    }

    public void OffPickAllTower()
    {
        foreach(GameObject tower in towerList)
        {
            tower.GetComponent<Tower>().PickTower(false);
        }
    } 
}
