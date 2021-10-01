using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question")]
public class Question : ScriptableObject
{
    [TextArea] public string question;
    public string correctAnswer;
    public string wrongAnswer1;
    public string wrongAnswer2;
    public string wrongAnswer3;
}
