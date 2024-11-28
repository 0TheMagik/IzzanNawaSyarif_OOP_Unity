using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // UI Elements
    public Text healthText;
    public Text pointsText;
    public Text waveText;
    public Text enemiesText;

    // Game variables
    private int playerHealth = 100;
    private int playerPoints = 0;
    private int currentWave = 1;
    private int enemiesInWave = 5;

    // Enemy data
    public class Enemy
    {
        public int level;
        public Enemy(int level)
        {
            this.level = level;
        }
    }

    void Start()
    {
        // Initialize UI
        UpdateUI();
    }

    public void PlayerTakesDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth < 0) playerHealth = 0;
        UpdateUI();
    }

    public void PlayerKillsEnemy(Enemy enemy)
    {
        playerPoints += enemy.level; // Points based on enemy level
        enemiesInWave--;
        if (enemiesInWave <= 0)
        {
            StartNewWave();
        }
        UpdateUI();
    }

    private void StartNewWave()
    {
        currentWave++;
        enemiesInWave = currentWave * 5; // Example logic: 5 enemies per wave
    }

    private void UpdateUI()
    {
        healthText.text = "Health: " + playerHealth;
        pointsText.text = "Points: " + playerPoints;
        waveText.text = "Wave: " + currentWave;
        enemiesText.text = "Enemies: " + enemiesInWave;
    }
}

