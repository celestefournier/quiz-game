using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] List<Difficulty> difficulties = new List<Difficulty>();
    [SerializeField] Text questionText;
    [SerializeField] Button[] buttons;
    [SerializeField] Text[] options;

    [Header("Buttons State")]
    [SerializeField] Sprite defaultButton;
    [SerializeField] Sprite wrongButton;
    [SerializeField] Sprite correctButton;

    List<Question> questions = new List<Question>();
    Question currentQuestion;
    int roundsLeft = 5;

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

        NextQuestion();
    }

    void NextQuestion()
    {
        if (roundsLeft <= 0) {
            GameOver();
            return;
        }

        currentQuestion = questions[Random.Range(0, questions.Count)];

        questionText.text = currentQuestion.question;

        List<string> answers = new List<string>();
        answers.Add(currentQuestion.correctAnswer);
        answers.Add(currentQuestion.wrongAnswer1);
        answers.Add(currentQuestion.wrongAnswer2);
        answers.Add(currentQuestion.wrongAnswer3);

        foreach (var option in options)
        {
            int answerIndex = Random.Range(0, answers.Count);
            option.text = answers[answerIndex];
            answers.Remove(answers[answerIndex]);
        }

        questions.Remove(currentQuestion);
        roundsLeft--;
    }

    public void SelectOption(GameObject button)
    {
        Text option = button.transform.Find("Text").GetComponent<Text>();

        if (option.text == currentQuestion.correctAnswer)
        {
            button.GetComponent<Image>().sprite = correctButton;
        } else
        {
            button.GetComponent<Image>().sprite = wrongButton;
        }

        foreach (var btn in buttons) btn.enabled = false;

        StartCoroutine("WaitForNextQuestion");
    }

    IEnumerator WaitForNextQuestion()
    {
        yield return new WaitForSeconds(2);

        foreach (var button in buttons)
        {
            button.enabled = true;
            button.gameObject.GetComponent<Image>().sprite = defaultButton;
        }

        NextQuestion();
    }

    void GameOver()
    {
        // Game Over
    }
}
