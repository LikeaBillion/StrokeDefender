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

    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();    
    }    
    void Start(){   
        currentHealth = playerHealth.GetHealth();
    }

    
    void Update()
    {            
        UpdateHealth();
        scoreText.text = scoreKeeper.GetScore().ToString("000000000");
    }

    void UpdateHealth(){
        if(currentHealth != playerHealth.GetHealth()){
            currentHealth = playerHealth.GetHealth();
            Destroy(Hearts[currentHealth]);
        }
    }
}
