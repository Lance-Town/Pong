using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Ball ballPrefab;
    private Ball ball;
    public Paddle paddlePrefab;
    private Paddle paddle1;
    private Paddle paddle2;
    public GameObject helpPanel; // panel to show help screen
    public GameObject winnerPanel; // panel to show game winner
    private bool helpScreenShowing = false;
    private bool isHKeyPressed = false; // boolean to see if h is pressed or not

    public static Vector2 bottomLeft;
    public static Vector2 topRight;

    private string[] tagsToToggle = {"Paddle", "Ball", "MiddleLine"};

    // Players scores
    public int player1Score = 0;
    public int player2Score = 0;

    // score needed to win game
    public int scoreToWin = 5;

    // text elements to display score and winner
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI winnerText;

    private Vector2 ballStartPos ;
    private Vector2 paddle1StartPos;
    private Vector2 paddle2StartPos;


    // Start is called before the first frame update
    void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0,0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // create a ball and paddle
        ball = Instantiate(ballPrefab);
        paddle1 = Instantiate(paddlePrefab) as Paddle;
        paddle2 = Instantiate(paddlePrefab) as Paddle;

        paddle1.init(true); // true = right paddle
        paddle2.init(false); // false = left paddle

        helpPanel.SetActive(false); // ensure the help panel is not showing at start of game
        winnerPanel.SetActive(false); // ensure the winner panel is not showing at start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape")) {
            Time.timeScale = 0;
            Application.Quit();
            enabled = false;
        } 

        if (Input.GetKeyDown(KeyCode.H) && !isHKeyPressed) {
            isHKeyPressed = true;
            ToggleHelp();
        }

        if (Input.GetKeyUp(KeyCode.H)) {
            isHKeyPressed = false;
        }
    }

    // Turn the help screen on and off
    void ToggleHelp() {
        if (helpScreenShowing) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    // Resume the game
    void ResumeGame() {
        helpScreenShowing = false;
        helpPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Pause the game
    void PauseGame() {
        helpScreenShowing = true;
        helpPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetRound() {
        Debug.Log("Resetting the Round...");
        ball.ResetBall();
        paddle1.ResetPaddle();
        paddle2.ResetPaddle();

        ball.gameObject.SetActive(true);
        paddle1.gameObject.SetActive(true);
        paddle2.gameObject.SetActive(true);

        Time.timeScale = 1;
    }

    private void UpdateScoreUI() {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    }

    public void PlayerScored(bool rightScore) {
        if (rightScore) {
            player1Score += 1;
        } else {
            // left player2 scored
            player2Score += 1;
        }

        // update the score board
        UpdateScoreUI();

        if (player1Score >= scoreToWin) {
            // player 1 won the game
            EndGame(1);
        } else if (player2Score >= scoreToWin) {
            // player 2 won the game
            EndGame(2);
        } else {
            ResetRound();
        }
    }

    void EndGame(int winner) {
        winnerPanel.SetActive(true);
        if (winner == 1) {
            winnerText.text = "Right Player Won!";
        } else {
            winnerText.text = "Left Player Won!";
        }

        ball.gameObject.SetActive(false);
        paddle1.gameObject.SetActive(false);
        paddle2.gameObject.SetActive(false);

        Time.timeScale = 0;
    }

    // Replay the game
    public void ReplayGame() {
        winnerPanel.SetActive(false);
        player1Score = 0;
        player2Score = 0;
        player1ScoreText.text = "0";
        player2ScoreText.text = "0";
        winnerText.text = "";
        ResetRound();
    }

    // handle button click to pull up help screen
    public void HandleHelpToggle() {
        ToggleHelp();
    }
}
