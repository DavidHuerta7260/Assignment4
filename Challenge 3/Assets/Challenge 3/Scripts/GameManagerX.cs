/*
David Huerta
Challenge 3
Handles score, win/lose messages, and restart.
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerX : MonoBehaviour
{
    public static GameManagerX Instance;

    [Header("State")]
    public bool isGameOver = false;
    public int score = 0;
    public int winScore = 30;

    [Header("UI")]
    public Text scoreText;
    public Text messageText;

    void Awake() { Instance = this; }

    void Start()
    {
        UpdateScoreUI();
        if (messageText) messageText.text = "";
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;
        score += amount;
        UpdateScoreUI();

        if (score >= winScore)
        {
            isGameOver = true;
            if (messageText) messageText.text = "You win!  Press R to try again!";
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        if (messageText) messageText.text = "You lose!  Press R to try again!";
    }

    void UpdateScoreUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
    }
}
