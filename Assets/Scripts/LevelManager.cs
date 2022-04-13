using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay =2f;
    ScoreKeeper scoreKeeper;
    Shooter[] shooters;
    AudioSource audioSource;
    Questions questions;
    PauseMenu pauseMenu;
    public string scene;


    void Awake() {

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        questions = FindObjectOfType<Questions>();
        audioSource =  FindObjectOfType<AudioSource>();
        shooters = FindObjectsOfType<Shooter>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        scene = SceneManager.GetActiveScene().name;        
    }

    void Start(){
        questions.HideQuestion();
        pauseMenu.noPause();
    }

    public void LoadLevel(string levelname){
        SceneManager.LoadScene(levelname);
        
    }

    public void MainMenu(){
        LoadLevel("MainMenu");
    }

    public void LevelSelect(){
        LoadLevel("LevelSelect");
    }

    public void Level1(){
        scoreKeeper.ResetScore();
        LoadLevel("Level 1");
    }

    public void GameOver(){
        StartCoroutine(WaitOnLoad("GameOver",sceneLoadDelay));
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit game");
    }

    public void PauseGame(){
        pauseMenu.Pause();
    }

     public void UnPauseGame(){
        pauseMenu.UnPause();
    }


    public void Question(){
        questions.ShowQuestion();
        for(int i=0; i<shooters.Length;i++){
            shooters[i].noFire = true;
        }
        Time.timeScale = 0.01f;
        audioSource.pitch = 0.5f; 
        
    }

    public void NoQuestion(){
        questions.HideQuestion();
        for(int i=0; i<shooters.Length;i++){
            shooters[i].noFire = false;
        }
        Time.timeScale = 1f;
        audioSource.pitch = 1f; 
        
    }

    IEnumerator WaitOnLoad(string levelname, float delay){
        yield return new WaitForSeconds(delay);
        LoadLevel(levelname);
    }
}
