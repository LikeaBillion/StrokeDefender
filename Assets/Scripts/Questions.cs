using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    //section for questions in the inspector
    [Header("Questions")]
    //sets question text
    [SerializeField] TextMeshProUGUI questionText;
    //sets questionso's from inspector
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    //storing current question of questionso type
    QuestionSO currentQuestion;

    //section for answers in the inspector
    [Header("Answers")]
    //list of button objects
    [SerializeField] GameObject[] answerButtons;
    //creates correctAnswerIndex and has answered early
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    //section for buttons in the inspector
    [Header("Button Colours")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    //section for timer in the inspector
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    //section for health changing in the inspector
    [Header("Health Change")]
    [SerializeField] Health playerHealth;

    
    void Awake()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update() {
        //timer gets filled by the fraction of time that has passed
        timerImage.fillAmount = timer.fillFraction;
        //if timer says load next question
        if(timer.loadNextQuestion){
            //resets value for loadnextquestion and hasanswered early
            hasAnsweredEarly = false;
            //getnextquestion called
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        //if nothing answered
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion){
            //display incorrect answer inputted
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index){
        //set has answered early
        hasAnsweredEarly = true;
        //checks answer
        DisplayAnswer(index);
        //disables buttons
        SetButtonState(false);
        //resets timer 
        timer.CancelTimer();
        

    }

    void GetNextQuestion(){
        //if there are still questions left
        if(questions.Count > 0){
            //enable pushing buttons
            SetButtonState(true);
            //set default sprites for that
            SetDefaultButtonSprites();
            //get a random question
            GetRandomQuestion();
            //display that question
            DisplayQuestion();   
        }
    }

    void GetRandomQuestion(){
        //gets a random index from the questions
        int index = Random.Range(0,questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion)){
            //remove once used
            questions.Remove(currentQuestion);
        }
    }

    private void DisplayQuestion(){
        //sets question text to current question
        questionText.text = currentQuestion.GetQuestion();
        
        for(int i = 0; i <answerButtons.Length; i++){
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            //displays current answer for each button
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state){
        //foreach button sets their state to true/false
        for (int i =0;i<answerButtons.Length;i++){
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites(){
        //foreach button sets their sprites to default
        for(int i=0;i < answerButtons.Length; i++){
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }

    void DisplayAnswer(int index){
        Image buttonImage;
        //if correct...
        if(index == currentQuestion.GetCorrectAnswerIndex()){
            //question text says answer is correct
            questionText.text = "Correct!";
            //changes button image to be correct answer sprite
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            //heals player
            playerHealth.Heal();
        }
        //if incorrect...
        else{
            //sets correctanswer as the index
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            //sets the correct answer as a string
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            //prints correct answer
            questionText.text = "Sorry, but the correct answer was;\n" + correctAnswer;
            //highlights correct answer
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            //makes player take damage
            playerHealth.TakeDamage(1);

        }
    }

    public void ShowQuestion(){
        //sets itself to active when called
        gameObject.SetActive(true);
        //starts/continues counting
        timer.counting = true;
        //resets button state
        SetButtonState(true);
    }

    public void HideQuestion(){
        //sets itself to inactive
        gameObject.SetActive(false);
        //stops timer counting
        timer.counting = false;
        //disables buttons
        SetButtonState(false);
    }
}



 
