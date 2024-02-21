using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2Int spawnRange;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private GameObject[] enemyPrefabs;

    private int spawnCount = 0;

    private void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        int spawnIndex = 0;
        while(true)
        {
            if(spawnCount == 0)
            {
                spawnCount = Random.Range(spawnRange.x, spawnRange.y);
                spawnIndex = Random.Range(0, enemyPrefabs.Length);
            }
            GameObject enemy = GameManager.instance.poolManager.GetPool(enemyPrefabs[spawnIndex].name);
            enemy.GetComponent<Enemy>().Init(wayPoints);
            spawnCount--;
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
