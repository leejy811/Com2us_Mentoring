using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public PlayerInfo player;
    public InputManager inputManager;
    public TowerSpawner towerSpawner;
    public PoolManager poolManager;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
