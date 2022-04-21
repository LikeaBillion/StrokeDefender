using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    //field for scoretext and highscoretext
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    //instances of scorekeeper and levelmanager
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start(){
        //get score from scorekeeper
        int score = scoreKeeper.GetScore();
        //gethighscore from scorekeeper
        int highscore = scoreKeeper.GetHighScore(scoreKeeper.nextLevel);
        //if highscore is smaller than score
        if(highscore <= score){
            scoreText.text = "";
            //show new highscore and change color of text
            highScoreText.text = "New Highscore: " + score;
            highScoreText.color = new Color(255,253,0,255);
        }

        else{
            //show score and highscore
            scoreText.text = "You scored: " + score;
            highScoreText.text = "Highscore: " + highscore;
        }

    }
    public void Next(){
        //when next is called the next indexed level is loaded
        scoreKeeper.nextLevelIndex = scoreKeeper.nextLevelIndex+1;
        levelManager.LoadLevelIndex(scoreKeeper.nextLevelIndex);
        
    }

}
