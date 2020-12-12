using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Transform parent;
    public float timeBetweenWaves = 5f;
    public Text waveCountdownText;
    private float countdown = 2f;
    private int waveIndex =0;
    void Update()
    {
        if(countdown<=0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown-=Time.deltaTime;
        waveCountdownText.text = Mathf.Floor(countdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;

        for(int i =0;i<waveIndex;i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Transform enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.SetParent(parent);
    }
}
