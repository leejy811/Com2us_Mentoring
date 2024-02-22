using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUIManager : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("IngameScene");
    }
}
