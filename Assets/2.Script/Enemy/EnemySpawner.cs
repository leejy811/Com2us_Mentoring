using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private GameObject[] enemyPrefabs;

    private void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            GameObject enemy = Instantiate(enemyPrefabs[0]);
            enemy.GetComponent<Enemy>().Init(wayPoints);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
