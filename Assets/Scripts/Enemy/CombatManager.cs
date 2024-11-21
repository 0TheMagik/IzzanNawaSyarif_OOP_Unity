using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;

    private void Start()
    {
        StartCoroutine(WaveSystem());
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private IEnumerator WaveSystem()
    {
        while (true)
        {
            yield return new WaitForSeconds(waveInterval);

            waveNumber++;
            totalEnemies = waveNumber * enemySpawners.Length;

            foreach (var spawner in enemySpawners)
            {
                spawner.defaultSpawnCount = waveNumber;
                spawner.isSpawning = true;
                StartCoroutine(spawner.SpawnEnemies());
            }

            yield return new WaitUntil(() => AllEnemiesDefeated());
        }
    }

    private bool AllEnemiesDefeated()
    {
        int currentTotalKills = 0;
        foreach (var spawner in enemySpawners)
        {
            currentTotalKills += spawner.totalKill;
        }
        return currentTotalKills >= totalEnemies;
    }
}
