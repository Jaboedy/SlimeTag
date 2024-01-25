using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class LobbySceneManager : MonoBehaviour
{
    [SerializeField] SlimeTagSceneManager sceneManager;
    [SerializeField] TMP_Text _joinCode;
	public void Awake()
	{
        sceneManager = FindObjectOfType<SlimeTagSceneManager>();
        _joinCode.text = $"Host Code: {sceneManager.getJoinCode()}";
		foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            Debug.Log($"Client: {client}");
        }
    }
}
