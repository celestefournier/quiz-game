using UnityEngine;

[System.Serializable]
public class Question
{
    [TextArea] public string question;
    [TextArea] public string correctAnswer;
    [TextArea] public string wrongAnswer1;
    [TextArea] public string wrongAnswer2;
    [TextArea] public string wrongAnswer3;
}
