using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SlimeTagSceneManager : NetworkBehaviour
{
    private string JoinCode;
    private NetworkVariable<int> currentMapIndex = new NetworkVariable<int>(0);
    private List<GameObject> players = new List<GameObject>();
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        DontDestroyOnLoad(gameObject);
    }
    public bool setJoinCode(string newJoinCode)
    {
        if (newJoinCode != null && newJoinCode.Length > 0)
        {
			this.JoinCode = newJoinCode;
		}
        if (this.JoinCode == newJoinCode)
        {
            return true;
        }
        return false;
    }

    public string getJoinCode()
    {
        if (!string.IsNullOrEmpty(this.JoinCode)) { 
            return this.JoinCode; 
        }
        return null;
    }

    public NetworkVariable<int> getCurrentMapIndex() {  return currentMapIndex; }
    public void setCurrentMapIndex(int newIndex)
    {
        currentMapIndex.Value = newIndex;
    }

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
    }

    public void RemovePlayer(GameObject player) { 
        players.Remove(player);
    }

    public List<GameObject> getPlayers()
    {
        return players;
    }
}
