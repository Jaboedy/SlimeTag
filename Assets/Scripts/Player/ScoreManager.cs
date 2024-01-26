using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine.Networking;
using Unity.Netcode.Components;
using Unity.Networking.Transport;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : NetworkBehaviour
{
    [SerializeField] Material[] TestMaterials;


    public int round;
    [SerializeField] private GameObject roundOver;
    [SerializeField] private GameObject roundBetweenDisplay;
    [SerializeField] private GameObject gameOverScreen;
    private SlimeTagSceneManager slimeTagSceneManager;

    public PlayersAndScores[] playersAndScores;
    public GameObject[] players;
    public string[] playerNames;
    public List<GameObject> infectedPlayers = new List<GameObject>();

    [SerializeField] private GameObject[] NetworkPlayers;

    public GameObject[] Maps;
    public GameObject currentMap;
    public List<GameObject> OldMaps = new List<GameObject>();

    public PlayersAndScores player1 = new PlayersAndScores();
    public PlayersAndScores player2 = new PlayersAndScores();
    public PlayersAndScores player3 = new PlayersAndScores();
    public PlayersAndScores player4 = new PlayersAndScores();
    public PlayersAndScores player5 = new PlayersAndScores();
	public override void OnNetworkSpawn()
	{
        slimeTagSceneManager = FindObjectOfType<SlimeTagSceneManager>();
        if (NetworkManager.Singleton.IsHost)
        {
            slimeTagSceneManager.setCurrentMapIndex(Random.Range(0, Maps.Length));
			Maps = GameObject.FindGameObjectsWithTag("Map");
			foreach (var map in Maps)
			{
				map.SetActive(false);
			}
			currentMap = Maps[slimeTagSceneManager.getCurrentMapIndex().Value];
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
    }

    void Update()
    {
        AssignPlayersWithScores();
        if (infectedPlayers.Count == players.Length - 1)
        {
            foreach(var player in slimeTagSceneManager.getPlayers())
            {
                if (player.GetComponent<PlayerMovement>().isInfected.Value == false)
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
            infectedPlayers.Clear();
            Time.timeScale = 0;

            var players = GetComponent<SlimeTagSceneManager>().getPlayers();
            foreach (var player in  players)
            {
                player.GetComponent<NetworkObject>().Despawn();
            }

            roundOver.SetActive(true);
            OldMaps.Add(currentMap);
            currentMap.SetActive(false);
            yield return new WaitForSecondsRealtime(3);
            roundOver.SetActive(false);
            roundBetweenDisplay.SetActive(true);
            yield return new WaitForSecondsRealtime(5);
            if (NetworkManager.Singleton.IsHost)
            {
                slimeTagSceneManager.setCurrentMapIndex(Random.Range(0, Maps.Length));
			}
            var newMap = Maps[slimeTagSceneManager.getCurrentMapIndex().Value];
            /*while (newMap == currentMap || OldMaps.Contains(newMap)) 
            {
                newMap = Maps[Random.Range(0, Maps.Length)];
            }*/
            currentMap = newMap;
            newMap.SetActive(true);
            foreach (var player in players)
            {
                player.GetComponent<PlayerMovement>().isInfected.Value = false;
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
    }

    private void AssignPlayersToSpawns()
    {
        if (!NetworkManager.Singleton.IsHost) { return; }
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = spawnPoints[i].transform.position;
            players[i].GetComponent<PlatformChaser2DModified>().startingPos = spawnPoints[i].transform;
            GameObject playerInstance = Instantiate(players[i], spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
            players[i].GetComponent<NetworkObject>().Spawn(playerInstance);
        }
        players[Random.Range(0, players.Length)].GetComponent<PlayerMovement>().BecomeInfected();

    }

    private void EndGame()
    {
        gameOverScreen.SetActive(true);
    }
}
