using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay =2f;

    //instances of other scripts
    ScoreKeeper scoreKeeper;
    Shooter[] shooters;
    AudioSource audioSource;
    Questions questions;
    PauseMenu pauseMenu;

    //current sceneIndex and scene name being recorded
    public string scene;
    public int sceneIndex;


    void Awake() {

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        questions = FindObjectOfType<Questions>();
        audioSource =  FindObjectOfType<AudioSource>();
        shooters = FindObjectsOfType<Shooter>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        scene = SceneManager.GetActiveScene().name;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;        
    }

    void Start(){
        //when started, both menus hide
        questions.HideQuestion();
        pauseMenu.noPause();
    }

    //updates to the next selected level from scorekeeper
    public void NextLevel(string scene){
        scoreKeeper.nextLevel = scene;
    }

    //loads the nextLevel from scorekeeper
    public void LoadNextLevel(){
        SceneManager.LoadScene(scoreKeeper.nextLevel);
    }
    
    //loads the level passed in and resets the score
    public void LoadLevel(string levelname){
        SceneManager.LoadScene(levelname);
        scoreKeeper.ResetScore();
    }

    //loads the level by index and resets the score
    public void LoadLevelIndex(int index){
        SceneManager.LoadScene(index);
        scoreKeeper.ResetScore();
    }

    //Loads mainmenu
    public void MainMenu(){
        LoadLevel("MainMenu");
    }
    //Loads levelselect
    public void LevelSelect(){
        LoadLevel("LevelSelect");
    }

    //Loads gameover with a delay
    public void GameOver(){
        StartCoroutine(WaitOnLoad("GameOver",sceneLoadDelay));
    }

    //Loads levelcompleted with a delay
     public void LevelCompleted(){
        StartCoroutine(WaitOnLoad("LevelCompleted",sceneLoadDelay));
    }

    //quits application- not working for webbrowsers
    public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit game");
    }

    //pauses the game
    public void PauseGame(){
        pauseMenu.Pause();
    }
    //unpauses the game
    public void UnPauseGame(){
        pauseMenu.UnPause();
    }

    //opens the question window and sets correct state for questions
    public void Question(){
        questions.ShowQuestion();
        for(int i=0; i<shooters.Length;i++){
            shooters[i].noFire = true;
        }
        Time.timeScale = 0.01f;
        audioSource.pitch = 0.5f; 
        
    }

    //closes the question window and sets defualt state for gameplay
    public void NoQuestion(){
        questions.HideQuestion();
        for(int i=0; i<shooters.Length;i++){
            shooters[i].noFire = false;
        }
        Time.timeScale = 1f;
        audioSource.pitch = 1f; 
        
    }

    //waits for a while before loading
    IEnumerator WaitOnLoad(string levelname, float delay){
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(levelname);
    }

    //gets the name of a scene from the index passed in
    public string NameFromIndex(int buildIndex){
        string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
}
