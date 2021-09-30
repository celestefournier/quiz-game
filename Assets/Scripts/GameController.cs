using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] List<Difficulty> difficulties = new List<Difficulty>();
    [SerializeField] Text questionText;
    [SerializeField] Text[] options;

    List<Question> questions = new List<Question>();

    void Start()
    {
        foreach (var difficulty in difficulties)
        {
            if (difficulty.name.ToLower() == DifficultyManager.difficulty)
            {
                questions = difficulty.questions;
                break;
            }
        }

        int index = Random.Range(0, questions.Count);

        List<string> answers = new List<string>();
        answers.Add(questions[index].correctAnswer);
        answers.Add(questions[index].wrongAnswer1);
        answers.Add(questions[index].wrongAnswer2);
        answers.Add(questions[index].wrongAnswer3);

        questionText.text = questions[index].question;

        foreach (var option in options)
        {
            int answerIndex = Random.Range(0, answers.Count);
            print(answers[answerIndex]);
            option.text = answers[answerIndex];
            answers.Remove(answers[answerIndex]);
        }
    }
}
