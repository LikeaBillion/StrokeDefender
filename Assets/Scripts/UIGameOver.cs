using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start(){
        int score = scoreKeeper.GetScore();
        int highscore = scoreKeeper.GetHighScore(scoreKeeper.nextLevel);
        if(highscore <= score){
            scoreText.text = "";
            highScoreText.text = "New Highscore: " + score;
            highScoreText.color = new Color(255,253,0,255);
        }

        else{
            scoreText.text = "You scored: " + score;
            highScoreText.text = "Highscore: " + highscore;
        }

    }
    public void Next(){
        scoreKeeper.nextLevelIndex = scoreKeeper.nextLevelIndex+1;
        levelManager.LoadLevelIndex(scoreKeeper.nextLevelIndex);
        
    }

}
