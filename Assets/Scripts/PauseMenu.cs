using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour

{   
    //gets instances of other scripts
    Shooter[] shooters;
    Player player;
    LevelManager levelManager;
    


    void Awake() {

        shooters = FindObjectsOfType<Shooter>();  
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
    
    }
    //when paused..
    public void Pause(){
        //sets the pausemenu active
        gameObject.SetActive(true);
        //stops shooting
        for(int i=0; i<shooters.Length;i++){
            shooters[i].noFire = true;
        }
        //sets timescale to zero to pause
        Time.timeScale = 0.00f;
        //sets the players state to paused
        player.paused = true;
    }

    //when not paused..
    public void UnPause(){
        //pausemenu is now inactive
        gameObject.SetActive(false);
        //sets all the shooters able to fire
        for(int i=0; i<shooters.Length;i++){
            shooters[i].noFire = false;
        }
        //resets timescale
        Time.timeScale = 1f;
        //sets player state to unpaused
        player.paused = false;
    }
    //function to set the object as inactive, used at the start of each level
    public void noPause(){
        gameObject.SetActive(false);
    }
    //function to return to the main menu
    public void pauseMainMenu(){
        Time.timeScale = 1f;
        levelManager.MainMenu();
    }
}
