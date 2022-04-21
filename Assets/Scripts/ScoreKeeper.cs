using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{   
    //instance of scorekeeper to be held
    static ScoreKeeper instance;

    //int for the currentscore of the level
    int score;

    //values for the next level
    public string nextLevel = "";   
    public int nextLevelIndex;

    //returns instance of scorekeeper
    public ScoreKeeper GetInstance(){
        return instance;
    }
    
    void Awake() {
        ManageSingleton();    
    }

    void ManageSingleton(){
        //if there is instance
        if(instance !=null){
            //destory gameobject
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else{
            //if theres not, this is instance
            instance = this;
            //don't destory it loading
            DontDestroyOnLoad(gameObject);
        }
    }

    //getter for score
    public int GetScore(){
        return score;
    }

    //function to change the score
    public void ModifyScore(int value){
        score += value;
        Mathf.Clamp(score,0,int.MaxValue);
    }

    //function to resetscore (between stages)
    public void ResetScore(){
        score =0;
    }


    //function to calculate the final score then compare to the highscore 
    public void finalScore(string scene){
        if(PlayerPrefs.GetInt(scene) < score){
            PlayerPrefs.SetInt(scene,score);
        }
    }

    public int GetHighScore(string scene){ 
        //returns highscore if one exists
        int hs = PlayerPrefs.GetInt(scene);
        if(hs > 0){
            return hs;
        }
        else{
            return 0;
        }
    }
}
