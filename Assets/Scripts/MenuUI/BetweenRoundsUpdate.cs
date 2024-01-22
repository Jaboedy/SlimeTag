using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BetweenRoundsUpdate : MonoBehaviour
{
    //get script that has all player scores tracking
    /*public PlayersAndScores[] playersAndScoresArray;
    private PlayersAndScores player1;
    private PlayersAndScores player2;
    private PlayersAndScores player3;
    private PlayersAndScores player4;
    private PlayersAndScores player5;*/
    
    [SerializeField] GameObject[] playersNames;
    [SerializeField] GameObject[] scores;


    
    void Start()
    {
        //player1 = new PlayersAndScores("playerOne", 10);
        
        //playersAndScoresArray = new PlayersAndScores[player1]
    }

    
    void Update()
    {
        
    }
}
//public class PlayersAndScores(string playerName, int score) { }
