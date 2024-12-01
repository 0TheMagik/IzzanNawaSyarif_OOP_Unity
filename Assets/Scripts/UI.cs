using UnityEngine;
using UnityEngine.UIElements;

public class GameUIManager : MonoBehaviour
{
    public int playerHealth = 100;
    public int playerPoints = 0;
    public int currentWave = 1;
    public int enemiesInWave = 0;

    private Label healthLabel;
    private Label pointsLabel;
    private Label waveLabel;
    private Label enemiesLabel;

    private void Start()
    {
        // Ambil referensi root VisualElement dari UI Toolkit
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Hubungkan elemen UI dengan variabel
        healthLabel = root.Q<Label>("HealthText");
        pointsLabel = root.Q<Label>("PointsText");
        waveLabel = root.Q<Label>("WaveText");
        enemiesLabel = root.Q<Label>("EnemiesText");

        // Update UI pada awal permainan
        UpdateUI();
    }

    public void UpdateHealth(int healthChange)
    {
        playerHealth += healthChange;
        UpdateUI();
    }

    public void AddPoints(int enemyLevel, int numberOfEnemies)
    {
        playerPoints += enemyLevel * numberOfEnemies;
        UpdateUI();
    }

    public void UpdateWave(int newWave, int enemyCount)
    {
        currentWave = newWave;
        enemiesInWave = enemyCount;
        UpdateUI();
    }

    public void EnemyDefeated()
    {
        if (enemiesInWave > 0)
        {
            enemiesInWave--;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Perbarui nilai teks pada UI Toolkit
        healthLabel.text = $"Health: {playerHealth}";
        pointsLabel.text = $"Points: {playerPoints}";
        waveLabel.text = $"Wave: {currentWave}";
        enemiesLabel.text = $"Enemies: {enemiesInWave}";
    }
}
