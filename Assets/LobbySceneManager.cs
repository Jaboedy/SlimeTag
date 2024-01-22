using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbySceneManager : NetworkBehaviour
{
    
	// Start is called before the first frame update
	public override void OnNetworkSpawn()
	{
        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            Debug.Log($"Client: {client}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
