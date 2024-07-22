using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public int waves;
    public int[] enemiesPerWave;
    public float spawnInterval;
    public float waveInterval;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < waves)
        {
            yield return StartCoroutine(SpawnEnemies());
            currentWave++;
            yield return new WaitForSeconds(waveInterval);
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave[currentWave]; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyIndex], spawnPoint.position, Quaternion.identity);
    }
}
