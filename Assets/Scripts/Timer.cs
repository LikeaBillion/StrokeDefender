using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //field for the time given for the question
    [SerializeField] float timeToCompleteQuestion = 0.1f;
    [SerializeField] float timeToCorrectAnswer = 0.05f;

    //variables to change how and when the timer should eb counting
    public bool loadNextQuestion;
    public float fillFraction;
    public bool isAnsweringQuestion;
    public bool counting;
    //currentvalue
    float timerValue;

    //levelmanager instance
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

    //called every frame
    void UpdateTimer(){

        if(counting){
            //if counting then remove time from the timer value
            timerValue -= Time.deltaTime;

            if(isAnsweringQuestion){
                //if answering question
                if(timerValue > 0){
                    //if timervalue not empty show the fill fraction to change
                    fillFraction = timerValue/timeToCompleteQuestion;
                }
                else{
                    //done answering question
                    isAnsweringQuestion = false;
                    //timer counts down time to correct answer
                    timerValue = timeToCorrectAnswer;
                }
            }
            //if not answering question
            else{
                if(timerValue > 0){
                    //if timervalue not 0 then change fillfraction
                    fillFraction = timerValue/timeToCorrectAnswer;

                }
                else{
                    //if complete... reset variables and call levelmanager to say question is finished
                    isAnsweringQuestion = true;
                    timerValue = timeToCompleteQuestion;
                    loadNextQuestion = true;
                    levelManager.NoQuestion(); 
                }
            }
        }
        
    }
}
