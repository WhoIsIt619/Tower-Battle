using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public EnemyTower enemyTower;
    public PlayerTower playerTower;

    public void LoadVictoryMenu()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void LoadDefeatMenu()
    {
        SceneManager.LoadScene("LoseScene");
    }

    private void Start()
    {
        enemyTower.gameManager = this;
        playerTower.gameManager = this;
    }
}
