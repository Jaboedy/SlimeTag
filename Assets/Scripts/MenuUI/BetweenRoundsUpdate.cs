using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class BetweenRoundsUpdate : MonoBehaviour
{
    public List<PlayersAndScores> scoreList = new List<PlayersAndScores>();

    public string winner;
    [SerializeField] private GameObject scoreManagerObject;
    private ScoreManager scoreManager;

    [SerializeField] private GameObject winnerDisplay;

    [Header("In Game Elements")]
    [SerializeField] GameObject playerOneName;
    [SerializeField] GameObject playerTwoName;
    [SerializeField] GameObject playerThreeName;
    [SerializeField] GameObject playerFourName;
    [SerializeField] GameObject playerFiveName;

    [SerializeField] GameObject playerOneScore;
    [SerializeField] GameObject playerTwoScore;
    [SerializeField] GameObject playerThreeScore;
    [SerializeField] GameObject playerFourScore;
    [SerializeField] GameObject playerFiveScore;

    [Header("In Between Rounds Elements")]
    [SerializeField] GameObject roundCounter;

    [SerializeField] GameObject playerOneNameRound;
    [SerializeField] GameObject playerTwoNameRound;
    [SerializeField] GameObject playerThreeNameRound;
    [SerializeField] GameObject playerFourNameRound;
    [SerializeField] GameObject playerFiveNameRound;

    [SerializeField] GameObject playerOneScoreRound;
    [SerializeField] GameObject playerTwoScoreRound;
    [SerializeField] GameObject playerThreeScoreRound;
    [SerializeField] GameObject playerFourScoreRound;
    [SerializeField] GameObject playerFiveScoreRound;

    private TextMeshProUGUI roundCounterText;

    private TextMeshProUGUI winnerDisplayText;

    private TextMeshProUGUI playerOneNameText;
    private TextMeshProUGUI playerTwoNameText;
    private TextMeshProUGUI playerThreeNameText;
    private TextMeshProUGUI playerFourNameText;
    private TextMeshProUGUI playerFiveNameText;

    private TextMeshProUGUI playerOneScoreText;
    private TextMeshProUGUI playerTwoScoreText;
    private TextMeshProUGUI playerThreeScoreText;
    private TextMeshProUGUI playerFourScoreText;
    private TextMeshProUGUI playerFiveScoreText;

    private TextMeshProUGUI playerOneNameRoundText;
    private TextMeshProUGUI playerTwoNameRoundText;
    private TextMeshProUGUI playerThreeNameRoundText;
    private TextMeshProUGUI playerFourNameRoundText;
    private TextMeshProUGUI playerFiveNameRoundText;

    private TextMeshProUGUI playerOneScoreRoundText;
    private TextMeshProUGUI playerTwoScoreRoundText;
    private TextMeshProUGUI playerThreeScoreRoundText;
    private TextMeshProUGUI playerFourScoreRoundText;
    private TextMeshProUGUI playerFiveScoreRoundText;


    void Start()
    {
        
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();

        roundCounterText = roundCounter.GetComponent<TextMeshProUGUI>();

        winnerDisplayText = winnerDisplay.GetComponent<TextMeshProUGUI>();

        playerOneNameText = playerOneName.GetComponent<TextMeshProUGUI>();
        playerTwoNameText = playerTwoName.GetComponent<TextMeshProUGUI>();
        playerThreeNameText = playerThreeName.GetComponent<TextMeshProUGUI>();
        playerFourNameText = playerFourName.GetComponent<TextMeshProUGUI>();
        playerFiveNameText = playerFiveName.GetComponent<TextMeshProUGUI>();

        playerOneNameRoundText = playerOneNameRound.GetComponent<TextMeshProUGUI>();
        playerTwoNameRoundText = playerTwoNameRound.GetComponent<TextMeshProUGUI>();
        playerThreeNameRoundText = playerThreeNameRound.GetComponent<TextMeshProUGUI>();
        playerFourNameRoundText = playerFourNameRound.GetComponent<TextMeshProUGUI>();
        playerFiveNameRoundText = playerFiveNameRound.GetComponent<TextMeshProUGUI>();

        playerOneScoreText = playerOneScore.GetComponent<TextMeshProUGUI>();
        playerTwoScoreText = playerTwoScore.GetComponent<TextMeshProUGUI>();
        playerThreeScoreText = playerThreeScore.GetComponent<TextMeshProUGUI>();
        playerFourScoreText = playerFourScore.GetComponent<TextMeshProUGUI>();
        playerFiveScoreText = playerFiveScore.GetComponent<TextMeshProUGUI>();

        playerOneScoreRoundText = playerOneScoreRound.GetComponent<TextMeshProUGUI>();
        playerTwoScoreRoundText = playerTwoScoreRound.GetComponent<TextMeshProUGUI>();
        playerThreeScoreRoundText = playerThreeScoreRound.GetComponent<TextMeshProUGUI>();
        playerFourScoreRoundText = playerFourScoreRound.GetComponent<TextMeshProUGUI>();
        playerFiveScoreRoundText = playerFiveScoreRound.GetComponent<TextMeshProUGUI>();

        scoreList.Add(scoreManager.player1);
        scoreList.Add(scoreManager.player2);
        scoreList.Add(scoreManager.player3);
        scoreList.Add(scoreManager.player4);
        scoreList.Add(scoreManager.player5);


    }


    void Update()
    {
        roundCounterText.text = ("Round " + scoreManager.GetComponent<ScoreManager>().round + " Result");
        if (scoreManager.playersAndScores[0] != null)
        {
            playerOneNameText.text = scoreManager.playersAndScores[0].playerName;
            playerTwoNameText.text = scoreManager.playersAndScores[1].playerName;
            playerThreeNameText.text = scoreManager.playersAndScores[2].playerName;
            playerFourNameText.text = scoreManager.playersAndScores[3].playerName;
            playerFiveNameText.text = scoreManager.playersAndScores[4].playerName;

            playerOneNameRoundText.text = scoreManager.playersAndScores[0].playerName;
            playerTwoNameRoundText.text = scoreManager.playersAndScores[1].playerName;
            playerThreeNameRoundText.text = scoreManager.playersAndScores[2].playerName;
            playerFourNameRoundText.text = scoreManager.playersAndScores[3].playerName;
            playerFiveNameRoundText.text = scoreManager.playersAndScores[4].playerName;

            playerOneScoreText.text = scoreManager.playersAndScores[0].score.ToString();
            playerTwoScoreText.text = scoreManager.playersAndScores[1].score.ToString();
            playerThreeScoreText.text = scoreManager.playersAndScores[2].score.ToString();
            playerFourScoreText.text = scoreManager.playersAndScores[3].score.ToString();
            playerFiveScoreText.text = scoreManager.playersAndScores[4].score.ToString();

            playerOneScoreRoundText.text = scoreManager.playersAndScores[0].score.ToString();
            playerTwoScoreRoundText.text = scoreManager.playersAndScores[1].score.ToString();
            playerThreeScoreRoundText.text = scoreManager.playersAndScores[2].score.ToString();
            playerFourScoreRoundText.text = scoreManager.playersAndScores[3].score.ToString();
            playerFiveScoreRoundText.text = scoreManager.playersAndScores[4].score.ToString();
        }
        if (scoreManager.player1 != null)
        {
            scoreList[0] = scoreManager.player1;
            scoreList[1] = scoreManager.player2;
            scoreList[2] = scoreManager.player3;
            scoreList[3] = scoreManager.player4;
            scoreList[4] = scoreManager.player5;
        }
        scoreList = scoreList.OrderBy(x => x.score).ToList();
        winner = scoreList.Last().playerName;
        winnerDisplayText.text = winner + "\n" + "Wins!";

    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

}

public class PlayersAndScores
{
    public string playerName;
    public int score;
    
}
