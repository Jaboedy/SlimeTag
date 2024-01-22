using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BetweenRoundsUpdate : MonoBehaviour
{
    //get script that has all player scores tracking
    public PlayersAndScores[] playersAndScoresArray;

    private PlayersAndScores player1 = new PlayersAndScores();
    private PlayersAndScores player2 = new PlayersAndScores();
    private PlayersAndScores player3 = new PlayersAndScores();
    private PlayersAndScores player4 = new PlayersAndScores();
    private PlayersAndScores player5 = new PlayersAndScores();

    [SerializeField] private GameObject PlayerOneName;
    [SerializeField] private GameObject PlayerTwoName;
    [SerializeField] private GameObject PlayerThreeName;
    [SerializeField] private GameObject PlayerFourName;
    [SerializeField] private GameObject PlayerFiveName;

    [SerializeField] private GameObject PlayerOneScore;
    [SerializeField] private GameObject PlayerTwoScore;
    [SerializeField] private GameObject PlayerThreeScore;
    [SerializeField] private GameObject PlayerFourScore;
    [SerializeField] private GameObject PlayerFiveScore;

    [SerializeField] GameObject[] playersNames;
    [SerializeField] GameObject[] scores;



    void Start()
    {
        playersNames = new GameObject[5];
        PlayersAndScores[] PlayersAndScoresArray = new PlayersAndScores[5];

        player1.playerName = "player1";
        player1.score = 0;

        player2.playerName = "player2";
        player2.score = 1;

        player3.playerName = "player3";
        player3.score = 2;

        player4.playerName = "player4";
        player4.score = 3;

        player5.playerName = "player5";
        player5.score = 4;

        PlayersAndScoresArray[0] = player1;
        PlayersAndScoresArray[1] = player2;
        PlayersAndScoresArray[2] = player3;
        PlayersAndScoresArray[3] = player4;
        PlayersAndScoresArray[4] = player5;
        for (int i = 0; i < PlayersAndScoresArray.Length; i++)
        {
            Debug.Log(PlayersAndScoresArray[i].playerName);
        }
    }


    void Update()
    {

    }
}

public class PlayersAndScores
{
    public string playerName;
    public int score;
    
}
