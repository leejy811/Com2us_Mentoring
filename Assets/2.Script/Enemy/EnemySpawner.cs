using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2Int spawnRange;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private GameObject[] enemyPrefabs;

    private int spawnCount = 0;

    private int waveIndex = 0;
    private int waveSpawnCount = 0;
    private int waveEnemyCount = 0;
    private int enemyCount = 0;

    private void Start()
    {
        NextWave();
        StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        int spawnIndex = 0;
        while(true)
        {
            if(waveSpawnCount != 0)
            {
                if (spawnCount == 0)
                {
                    spawnCount = Random.Range(spawnRange.x, spawnRange.y);
                    spawnIndex = Random.Range(0, enemyPrefabs.Length);
                }
                GameObject enemy = GameManager.instance.poolManager.GetPool(enemyPrefabs[spawnIndex].name);
                waveSpawnCount--;
                enemy.GetComponent<Enemy>().Init(wayPoints);
                spawnCount--;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }
    
    public void NextWave()
    {
        if (waveEnemyCount != 0) return;

        waveIndex++;
        waveSpawnCount = waveIndex * 5;
        waveEnemyCount = waveIndex * 5;
        GameManager.instance.uiManager.SetWaveInfo(waveIndex, waveEnemyCount, waveIndex * 5);
    }

    public void DieEnemy()
    {
        waveEnemyCount--;
        GameManager.instance.uiManager.SetWaveInfo(waveIndex, waveEnemyCount, waveIndex * 5);
    }
}
