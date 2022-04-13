using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour

{
    Shooter[] shooters;
    Player player;
    LevelManager levelManager;
    


    void Awake() {

        shooters = FindObjectsOfType<Shooter>();  
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
    
    }

    public void Pause(){
        gameObject.SetActive(true);
        for(int i=0; i<shooters.Length;i++){
            shooters[i].noFire = true;
        }
        Time.timeScale = 0.00f;
        player.paused = true;
    }

    public void UnPause(){
        gameObject.SetActive(false);
        for(int i=0; i<shooters.Length;i++){
            shooters[i].noFire = false;
        }
        Time.timeScale = 1f;
        player.paused = false;
    }

    public void noPause(){
        gameObject.SetActive(false);
    }

    public void pauseMainMenu(){
        Time.timeScale = 1f;
        levelManager.MainMenu();
    }
}
