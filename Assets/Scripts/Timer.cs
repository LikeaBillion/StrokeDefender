using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 0.1f;
    [SerializeField] float timeToCorrectAnswer = 0.05f;

    public bool loadNextQuestion;
    public float fillFraction;

    public bool isAnsweringQuestion;
    public bool counting;
    float timerValue;

    LevelManager levelManager;

    void Start() {
        levelManager = FindObjectOfType<LevelManager>();
        timerValue = timeToCompleteQuestion;
        isAnsweringQuestion = true;
        loadNextQuestion = true;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer(){
        timerValue = 0;
    }

    void UpdateTimer(){

        if(counting){
            timerValue -= Time.deltaTime;

            if(isAnsweringQuestion){
                if(timerValue > 0){
                    fillFraction = timerValue/timeToCompleteQuestion;
                }
                else{
                    isAnsweringQuestion = false;
                    timerValue = timeToCorrectAnswer;
                }
            }
            else{
                if(timerValue > 0){
                    fillFraction = timerValue/timeToCorrectAnswer;

                }
                else{
                
                    isAnsweringQuestion = true;
                    timerValue = timeToCompleteQuestion;
                    loadNextQuestion = true;
                    levelManager.NoQuestion(); 
                }
            }
        }
        
    }
}
