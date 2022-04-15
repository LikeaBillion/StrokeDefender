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
        questions.HideQuestion();
        pauseMenu.noPause();
    }

    public void NextLevel(string scene){
        scoreKeeper.nextLevel = scene;
    }

    public void LoadNextLevel(){
        SceneManager.LoadScene(scoreKeeper.nextLevel);
    }
    

    public void LoadLevel(string levelname){
        SceneManager.LoadScene(levelname);
        scoreKeeper.ResetScore();
    }

    public void LoadLevelIndex(int index){
        SceneManager.LoadScene(index);
        scoreKeeper.ResetScore();
    }


    public void MainMenu(){
        LoadLevel("MainMenu");
    }

    public void LevelSelect(){
        LoadLevel("LevelSelect");
    }

    public void GameOver(){
        StartCoroutine(WaitOnLoad("GameOver",sceneLoadDelay));
    }

     public void LevelCompleted(){
        StartCoroutine(WaitOnLoad("LevelCompleted",sceneLoadDelay));
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
        SceneManager.LoadScene(levelname);
    }

    public string NameFromIndex(int buildIndex){
        string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
}
