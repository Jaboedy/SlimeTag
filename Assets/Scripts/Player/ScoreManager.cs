using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public PlayersAndScores[] playersAndScores;
    public GameObject[] players;
    public string[] playerNames;

    private PlayersAndScores player1 = new PlayersAndScores();
    private PlayersAndScores player2 = new PlayersAndScores();
    private PlayersAndScores player3 = new PlayersAndScores();
    private PlayersAndScores player4 = new PlayersAndScores();
    private PlayersAndScores player5 = new PlayersAndScores();
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        playerNames = new string[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            playerNames[i] = players[i].GetComponent<PlayerScoreManager>().enteredName;
        }

        playersAndScores = new PlayersAndScores[players.Length];
        StartCoroutine(WaitForTesting());
    }


    void Update()
    {
        AssignPlayersWithScores();
    }

    private void AssignPlayersWithScores()
    {
        player1.playerName = playerNames[0];
        player1.score = players[0].GetComponent<PlayerScoreManager>().TotaledScore;
        playersAndScores[0] = player1;

        if (players.Length >= 2)
        {
            player2.playerName = playerNames[1];
            player2.score = players[1].GetComponent<PlayerScoreManager>().TotaledScore;
            playersAndScores[1] = player2;
        }
        if (players.Length >= 3)
        {
            player3.playerName = playerNames[2];
            player3.score = players[2].GetComponent<PlayerScoreManager>().TotaledScore;
            playersAndScores[2] = player3;
        }
        if (players.Length >= 4)
        {
            player4.playerName = playerNames[3];
            player4.score = players[3].GetComponent<PlayerScoreManager>().TotaledScore;
            playersAndScores[3] = player4;
        }
        if (players.Length >=5)
        {
            player5.playerName = playerNames[4];
            player5.score = players[4].GetComponent<PlayerScoreManager>().TotaledScore;
            playersAndScores[4] = player5;
        }
    }

    private IEnumerator WaitForTesting()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(playersAndScores[0].playerName.ToString() + " " + playersAndScores[0].score.ToString());
        Debug.Log(playersAndScores[1].playerName.ToString() + " " + playersAndScores[1].score.ToString());
        Debug.Log(playerNames[0].ToString() + " " + playerNames[1].ToString());
        Debug.Log(players[0].ToString() + " " + players[1].ToString());
    }
}
