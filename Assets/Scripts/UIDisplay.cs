using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    //fieled header for health, covers all the health elements
    [Header("Health")]
    [SerializeField] List<GameObject> Hearts;
    [SerializeField] Health playerHealth;
    int currentHealth;

    //fieled header for score, covers the score text and scorekeeper instance
    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    //fieled header for progress, covers the progress bar adn wave text. Including instance of enemy spawner
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
        //changes score as it increases
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
        //bar changes based of waves completed from enemy spawner
        progressBar.value = (float)enemySpawner.GetCurrentWaveCount()/enemySpawner.GetNumberOfWaves();
        //text changes to number of waves done
        waveText.text = "Wave: " + (enemySpawner.GetCurrentWaveCount()+1);

    }

    void UpdateHealth(){
        //if health has gone down
        if(currentHealth > playerHealth.GetHealth()){
            //sets current health to be the new lower value for health
            currentHealth = playerHealth.GetHealth();
            //removes one heart container
            Hearts[currentHealth].SetActive(false);
        }
        //if health has increased
        else if(currentHealth < playerHealth.GetHealth()){
            //reset health hearts
            currentHealth = playerHealth.GetHealth();
            for(int i=0;i<Hearts.Count;i++){
                Hearts[i].SetActive(true);
            }
        }
    }
}
