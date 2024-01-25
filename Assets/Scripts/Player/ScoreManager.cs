using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Material[] TestMaterials;


    public int round;
    [SerializeField] private GameObject roundOver;
    [SerializeField] private GameObject roundBetweenDisplay;
    [SerializeField] private GameObject gameOverScreen;

    public PlayersAndScores[] playersAndScores;
    public GameObject[] players;
    public string[] playerNames;
    public List<GameObject> infectedPlayers = new List<GameObject>();

    public GameObject[] Maps;
    public GameObject currentMap;
    public List<GameObject> OldMaps = new List<GameObject>();

    public PlayersAndScores player1 = new PlayersAndScores();
    public PlayersAndScores player2 = new PlayersAndScores();
    public PlayersAndScores player3 = new PlayersAndScores();
    public PlayersAndScores player4 = new PlayersAndScores();
    public PlayersAndScores player5 = new PlayersAndScores();
    void Start()
    {
        Maps = GameObject.FindGameObjectsWithTag("Map");
        foreach (var map in Maps)
        {
            map.SetActive(false);
        }
        currentMap = Maps[Random.Range(0, Maps.Length)];
        currentMap.SetActive(true);
        players = GameObject.FindGameObjectsWithTag("Player");
        AssignPlayersToSpawns();
        playerNames = new string[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            playerNames[i] = players[i].GetComponent<PlayerScoreManager>().enteredName;
        }

        playersAndScores = new PlayersAndScores[players.Length];

        round = 1;
        StartCoroutine(WaitForTesting());
    }

    void Update()
    {
        AssignPlayersWithScores();
        if (infectedPlayers.Count == players.Length - 1)
        {
            foreach(var player in players)
            {
                if (player.GetComponent<PlayerMovement>().isInfected == false)
                {
                    player.GetComponent<PlayerScoreManager>().TotaledScore += 10;
                }
            }
            StartCoroutine(RoundOver());
        }
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
    private IEnumerator RoundOver()
    {
        if (round < players.Length)
        {
            Debug.Log("Running RoundOver Coroutine");
            infectedPlayers.Clear();
            Time.timeScale = 0;

            roundOver.SetActive(true);
            OldMaps.Add(currentMap);
            currentMap.SetActive(false);
            yield return new WaitForSecondsRealtime(3);
            roundOver.SetActive(false);
            roundBetweenDisplay.SetActive(true);
            yield return new WaitForSecondsRealtime(5);
            var newMap = Maps[Random.Range(0, Maps.Length)];
            /*while (newMap == currentMap || OldMaps.Contains(newMap)) 
            {
                newMap = Maps[Random.Range(0, Maps.Length)];
            }*/
            currentMap = newMap;
            newMap.SetActive(true);
            foreach (var player in players)
            {
                player.GetComponent<PlayerMovement>().isInfected = false;
                player.GetComponent<SpriteRenderer>().material = TestMaterials[Random.Range(0, TestMaterials.Length)];
            }
            AssignPlayersToSpawns();
            roundBetweenDisplay.SetActive(false);
            round += 1;
            yield return new WaitForSecondsRealtime(1);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            EndGame();
        }


        //Break This Function Up
    }

    private IEnumerator WaitForTesting()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(playersAndScores[0].playerName.ToString() + " " + playersAndScores[0].score.ToString());
        Debug.Log(playersAndScores[1].playerName.ToString() + " " + playersAndScores[1].score.ToString());
        Debug.Log(playersAndScores[2].playerName.ToString() + " " + playersAndScores[2].score.ToString());
        Debug.Log(playersAndScores[3].playerName.ToString() + " " + playersAndScores[3].score.ToString());
        Debug.Log(playersAndScores[4].playerName.ToString() + " " + playersAndScores[4].score.ToString());
    }

    private void AssignPlayersToSpawns()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawnPoints[i].transform.position;
            players[i].GetComponent<PlatformChaser2DModified>().startingPos = spawnPoints[i].transform;
        }
        players[Random.Range(0, players.Length)].GetComponent<PlayerMovement>().BecomeInfected();
    }

    private void EndGame()
    {
        gameOverScreen.SetActive(true);
    }
}
