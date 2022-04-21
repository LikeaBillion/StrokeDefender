using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{    
    //fields for all the objects used on the page
    [SerializeField] TextMeshProUGUI highscore;
    [SerializeField] GameObject[] levelButtons;
    [SerializeField] Sprite defaultButtonSprite;
    [SerializeField] Sprite selectedButtonSprite;
    [SerializeField] GameObject background;
    [SerializeField] Sprite[] backgrounds;

    //instances of scorekeeper and levelmanager
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        //updates highscore each frame if changes
        highscore.text = "Highscore: " + scoreKeeper.GetHighScore(scoreKeeper.nextLevel);
    }

    //changes the value for next selected level to go to, and handles the buttons working correctly as intended
    public void OnSelected(int index){
        scoreKeeper.nextLevel = "";
        //sets all buttons to default sprites
        for(int i =0; i<levelButtons.Length;i++){
            levelButtons[i].GetComponent<Image>().sprite = defaultButtonSprite;
        }
        //sets selected button as selected sprite
        levelButtons[index].GetComponent<Image>().sprite = selectedButtonSprite;
        //next index is the index selected (+4 due the mainmenu, levelselect ,gameover and level completed scenes)
        scoreKeeper.nextLevelIndex = index+4;
        //gets the scene name from index
        string scene = levelManager.NameFromIndex(index+4);
        ChangeBackground(index);
        //sets the next level to the scene
        levelManager.NextLevel(scene);
    }

    //changes background based on the button selected
    void ChangeBackground(int index){
        //gets the objects sprite renderer
        SpriteRenderer image = background.transform.Find("sky").GetComponent<SpriteRenderer>();
        //changes to the image from the list passed in
        image.sprite = backgrounds[index];
    }

}
