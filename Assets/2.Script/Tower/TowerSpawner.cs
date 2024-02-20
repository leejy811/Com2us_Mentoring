using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerSpawner : MonoBehaviour
{
    bool isPickTower = false;

    int curPickIndex;
    GameObject curPickTower;
    List<GameObject> towerList;
    [SerializeField] GameObject[] towerPrefabs;
    [SerializeField] GameObject[] towerIconPrefabs;

    void Start()
    {
        towerList = new List<GameObject>();
    }

    public void SpawnTower(Transform spawnPos)
    {
        if (!isPickTower) return;
        towerList.Add(Instantiate(towerPrefabs[curPickIndex], spawnPos.position, Quaternion.identity));
        isPickTower = false;
        Destroy(curPickTower);
    }

    public void ChangePickState(int index)
    {
        if (isPickTower) return;
        isPickTower = true;

        curPickTower = Instantiate(towerIconPrefabs[index]);
        curPickIndex = index;
    }

    public void ChangeTowerColor(Color color)
    {
        if (!isPickTower) return;

        curPickTower.GetComponentsInChildren<SpriteRenderer>()[1].color = color;
    }
}
