using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu, difficulty;

    public void DifficultySelection()
    {
        menu.SetActive(false);
        difficulty.SetActive(true);
    }

    public void StartGame(string difficulty)
    {
        DifficultyManager.difficulty = difficulty;
        SceneManager.LoadScene("Game");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
