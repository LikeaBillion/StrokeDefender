using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{    
    [SerializeField] TextMeshProUGUI highscore;
    [SerializeField] GameObject[] levelButtons;
    [SerializeField] Sprite defaultButtonSprite;
    [SerializeField] Sprite selectedButtonSprite;
    [SerializeField] GameObject background;
    [SerializeField] Sprite[] backgrounds;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    private void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        highscore.text = "Highscore: " + scoreKeeper.GetHighScore(scoreKeeper.nextLevel);
    }

    public void OnSelected(int index){
        scoreKeeper.nextLevel = "";
        for(int i =0; i<levelButtons.Length;i++){
            levelButtons[i].GetComponent<Image>().sprite = defaultButtonSprite;
        }
        levelButtons[index].GetComponent<Image>().sprite = selectedButtonSprite;
        scoreKeeper.nextLevelIndex = index+4;
        string scene = levelManager.NameFromIndex(index+4);
        ChangeBackground(index);
        levelManager.NextLevel(scene);
    }

    void ChangeBackground(int index){
        SpriteRenderer image = background.transform.Find("sky").GetComponent<SpriteRenderer>();
        image.sprite = backgrounds[index];
    }

}
