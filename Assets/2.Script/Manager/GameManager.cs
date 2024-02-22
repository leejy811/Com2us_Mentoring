using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    public PlayerInfo player;
    public InputManager inputManager;
    public TowerSpawner towerSpawner;
    public EnemySpawner enemySpawner;
    public PoolManager poolManager;
    public UIManager uiManager;
    public DBManager dbManager;

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
        StartCoroutine(uiManager.GameOver(2f));
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
