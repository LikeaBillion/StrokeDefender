using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay =2f;
    ScoreKeeper scoreKeeper;

    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();    
    }

    public void LoadLevel(string levelname){
        SceneManager.LoadScene(levelname);
    }

    public void MainMenu(){
        LoadLevel("MainMenu");
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

    IEnumerator WaitOnLoad(string levelname, float delay){
        yield return new WaitForSeconds(delay);
        LoadLevel(levelname);
    }
}
