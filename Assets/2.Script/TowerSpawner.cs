using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TowerSpawner : MonoBehaviour
{
    bool isPickTower = false;
    float distance = 15f;
    Vector3 mousePos;
    Camera main;

    int curPickIndex;
    GameObject curPickTower;
    List<GameObject> towerList;
    [SerializeField] GameObject[] towerPrefabs;
    [SerializeField] GameObject[] towerIconPrefabs;

    void Start()
    {
        main = Camera.main;
        towerList = new List<GameObject>();
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, distance);
        if (!hit || !isPickTower) return;

        if (Input.GetMouseButtonDown(0) && hit.collider.tag == "BackGround")
        {
            SpawnTower(hit.transform);
        }
        else
        {
            if(hit.collider.tag == "BackGround")
            {
                curPickTower.GetComponentsInChildren<SpriteRenderer>()[1].color = Color.black;
            }
            else if (hit.collider.tag == "Road")
            {
                curPickTower.GetComponentsInChildren<SpriteRenderer>()[1].color = Color.white;
            }
        }
    }

    void SpawnTower(Transform spawnPos)
    {
        Tile tile = spawnPos.gameObject.GetComponentInParent<Tile>();
        if (tile.isHaveTower) return;

        tile.isHaveTower = true;
        towerList.Add(Instantiate(towerPrefabs[curPickIndex], spawnPos.position, Quaternion.identity));
        isPickTower = false;
        Destroy(curPickTower);
    }

    public void ChangePickState(int index)
    {
        if (isPickTower) return;
        isPickTower = true;

        curPickTower = Instantiate(towerIconPrefabs[index], new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
        curPickIndex = index;
    }
}
