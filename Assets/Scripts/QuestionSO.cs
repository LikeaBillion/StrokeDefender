using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//creating questions scriptable object
[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    //parametters for inspector
    [TextArea(2,6)]  
    [SerializeField] string question = "Default text";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

    //getters for all important values
    public string GetQuestion(){
        return question;
    }

    public string GetAnswer(int index){
        return answers[index];
    }

    public int GetCorrectAnswerIndex(){
        return correctAnswerIndex;
    }
    
}
