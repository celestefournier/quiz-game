using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] List<Difficulty> difficulties = new List<Difficulty>();

    [Header("Screens")]
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject gameOverScreen;

    [Header("Questions")]
    [SerializeField] Text questionText;
    [SerializeField] Button tipButton;
    [SerializeField] Button[] buttons;
    [SerializeField] Text[] options;
    [SerializeField] CanvasGroup canvas;

    [Header("Buttons State")]
    [SerializeField] Sprite defaultButton;
    [SerializeField] Sprite wrongButton;
    [SerializeField] Sprite correctButton;

    [Header("Score")]
    [SerializeField] Text scoreText;
    [SerializeField] Text bestScoreText;

    [Header("Score Game Over")]
    [SerializeField] Text scoreGameOverText;
    [SerializeField] Text bestScoreGameOverText;

    List<Question> questions = new List<Question>();
    Difficulty currentDifficulty;
    Question currentQuestion;
    int roundsLeft;
    int score = 0;
    int bestScore;
    int tips = 3;

    void Start()
    {
        foreach (var difficulty in difficulties)
        {
            if (difficulty.name.ToLower() == DifficultyManager.difficulty)
            {
                currentDifficulty = difficulty;
                questions = difficulty.questions;
                roundsLeft = questions.Count;
                break;
            }
        }

        bestScore = PlayerPrefs.GetInt(currentDifficulty.name, 0);
        bestScoreText.text = $"RECORDE: {bestScore}";

        NextQuestion();
    }

    void NextQuestion()
    {
        if (roundsLeft <= 0) {
            GameOver();
            return;
        }

        foreach (var button in buttons)
        {
            button.interactable = true;
        }

        tipButton.interactable = true;

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
            score += currentDifficulty.score;
            scoreText.text = $"PONTOS: {score}";
        } else
        {
            button.GetComponent<Image>().sprite = wrongButton;
        }

        canvas.blocksRaycasts = false;

        StartCoroutine("WaitForNextQuestion");
    }

    IEnumerator WaitForNextQuestion()
    {
        yield return new WaitForSeconds(1);

        canvas.blocksRaycasts = true;

        foreach (var button in buttons)
        {
            button.gameObject.GetComponent<Image>().sprite = defaultButton;
        }

        NextQuestion();
    }

    void GameOver()
    {
        gameScreen.SetActive(false);
        gameOverScreen.SetActive(true);
        scoreGameOverText.text = score.ToString();

        if (score > bestScore)
        {
            PlayerPrefs.SetInt(currentDifficulty.name, score);
            bestScoreGameOverText.text = score.ToString();
        } else
        {
            bestScoreGameOverText.text = bestScore.ToString();
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Tip(Text tipText)
    {
        if (tips <= 0) {
            return;
        }

        int removedOptions = 0;
        int index;

        while(removedOptions < 2)
        {
            index = Random.Range(0, options.Length);
            Button button = options[index].gameObject.transform.parent.GetComponent<Button>();

            if (options[index].text != currentQuestion.correctAnswer && button.interactable)
            {
                button.interactable = false;
                removedOptions++;
            }
        }

        tips--;
        tipText.text = tips.ToString();
        tipButton.interactable = false;
    }
}
