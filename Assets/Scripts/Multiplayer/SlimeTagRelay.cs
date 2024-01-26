using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.Networking;

public class SlimeTagRelay : MonoBehaviour
{
    [SerializeField] private TMP_Text _joinCodeText;
    [SerializeField] private TMP_InputField _joinInput;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _slimeTagSceneManager;

    private UnityTransport _transport;
    private const int MaxPlayers = 5;
    public string joinCode = "";

    private async void Awake()
    {
        DontDestroyOnLoad(this);
        _transport = FindObjectOfType<UnityTransport>();
        _buttons.SetActive(false);
        await Authenticate();
        
        _buttons.SetActive(true);
    }

	private async Task Authenticate()
    {
        await UnityServices.InitializeAsync();
		await AuthenticationService.Instance.SignInAnonymouslyAsync();
	}

    public async void CreateGame()
    {
        _buttons.SetActive(false);

        Allocation a = await RelayService.Instance.CreateAllocationAsync(MaxPlayers);
        joinCode = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);
        Debug.Log(joinCode);

        _joinCodeText.SetText($"Host Code: {joinCode}");

        _transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

        bool serverStarted = NetworkManager.Singleton.StartHost();
        if (serverStarted)
        {
            _slimeTagSceneManager.GetComponent<SlimeTagSceneManager>().setJoinCode(joinCode);
            NetworkManager.Singleton.SceneManager.LoadScene("Lobby", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
	}

    //Currently doesn't let client join session.  Only stopped working after changing scene added to CreateGame function
    public async void JoinGame()
    {
        _buttons.SetActive(false);

        JoinAllocation a = await RelayService.Instance.JoinAllocationAsync(_joinInput.text);
        _slimeTagSceneManager.GetComponent<SlimeTagSceneManager>().setJoinCode(_joinInput.text);

        _transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData, a.HostConnectionData);

        NetworkManager.Singleton.StartClient();
    }

    public List<string> GetConnectedPlayers()
    {
        List<string> players = new List<string>();
        foreach (var item in NetworkManager.Singleton.ConnectedClientsList)
        {
            players.Add(item.ClientId.ToString());
        }
        return players;
    }
}
