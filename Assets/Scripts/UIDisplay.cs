using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] List<GameObject> Hearts;
    [SerializeField] Health playerHealth;
    int currentHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress")]
    [SerializeField] Slider progressBar;
    [SerializeField] TextMeshProUGUI waveText;
    EnemySpawner enemySpawner;


    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();    
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }    
    void Start(){   
        currentHealth = playerHealth.GetHealth();
    }

    
    void Update()
    {            
        UpdateHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
        progressBar.value = (float)enemySpawner.GetCurrentWaveCount()/enemySpawner.GetNumberOfWaves();
        waveText.text = "Wave: " + enemySpawner.GetCurrentWaveCount();

    }

    void UpdateHealth(){
        if(currentHealth != playerHealth.GetHealth()){
            currentHealth = playerHealth.GetHealth();
            Destroy(Hearts[currentHealth]);
        }
    }
}
