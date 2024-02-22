using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    float distance = 15f;
    Vector3 mousePos;
    Camera main;
    TowerSpawner towerSpawner;

    void Start()
    {
        main = Camera.main;
        towerSpawner = GameManager.instance.towerSpawner;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos = main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, transform.forward, distance);
        if (!hit) return;

        if (Input.GetMouseButtonDown(0))
        {
            if(hit.collider.tag == "BackGround")
            {
                towerSpawner.SpawnTower(hit.transform);
            }
            else if (hit.collider.tag == "Tower" && !towerSpawner.isPickTower)
            {
                towerSpawner.SetPickTower(true, hit.transform.gameObject);
                towerSpawner.OffPickAllTower();
                hit.transform.GetComponent<Tower>().PickTower(true);
            }
        }
        else
        {
            if (hit.collider.tag == "BackGround")
            {
                towerSpawner.ChangeTowerColor(Color.black);
            }
            else
            {
                towerSpawner.ChangeTowerColor(Color.white);
            }
        }
    }
}
