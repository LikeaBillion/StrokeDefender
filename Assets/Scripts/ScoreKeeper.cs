using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{   
    static ScoreKeeper instance;
    int score =0;

    public ScoreKeeper GetInstance(){
        return instance;
    }
    
    void Awake() {
        ManageSingleton();    
    }

    void ManageSingleton(){
        if(instance !=null){
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else{
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore(){
        return score;
    }

    public void ModifyScore(int value){
        score += value;
        Mathf.Clamp(score,0,int.MaxValue);
    }

    public void ResetScore(){
        score =0;
    }

    public void finalScore(string scene){
        if(PlayerPrefs.GetInt(scene) < score){
            PlayerPrefs.SetInt(scene,score);
        }
    }
}
