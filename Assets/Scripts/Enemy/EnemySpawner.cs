using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            int enemiesToSpawn = defaultSpawnCount * spawnCountMultiplier;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                SpawnEnemy();
            }

            spawnCount += enemiesToSpawn;

            if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
            {
                spawnCountMultiplier += multiplierIncreaseCount;
                totalKillWave = 0;
            }
        }
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            Enemy newEnemy = Instantiate(spawnedEnemy, transform.position, Quaternion.identity);

            if (combatManager != null)
            {
                newEnemy.SetCombatManager(combatManager);
            }

            newEnemy.OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        totalKill++;
        totalKillWave++;
        enemy.OnDeath -= OnEnemyDeath;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }
}

