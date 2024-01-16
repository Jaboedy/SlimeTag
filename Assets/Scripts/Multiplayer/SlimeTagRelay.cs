using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class SlimeTagRelay : MonoBehaviour
{
    [SerializeField] private TMP_Text _joinCodeText;
    [SerializeField] private TMP_InputField _joinInput;
    [SerializeField] private GameObject _buttons;

    private UnityTransport _transport;
    private const int MaxPlayers = 5;

    private async void Awake()
    {
        _transport = FindObjectOfType<UnityTransport>();
        Debug.Log(_transport);
        _buttons.SetActive(false);
        Debug.Log("Authenticating");
        await Authenticate();
        Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
        
        _buttons.SetActive(true);
    }

	private void Start()
	{
        StartCoroutine(WaitThenCreateGame(5f));
	}

	private async Task Authenticate()
    {
        try
		{
			await UnityServices.InitializeAsync();
		}
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        Debug.Log("InitializeAsync complete");
		await AuthenticationService.Instance.SignInAnonymouslyAsync();
		Debug.Log("Signin Anonymously succeeded");
		Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
	}

    private IEnumerator WaitThenCreateGame(float seconds)
    {
        Debug.Log("Start wait");
        yield return new WaitForSeconds(seconds);
        Debug.Log("End wait");
		CreateGame();
	}

    public async void CreateGame()
    {
        _buttons.SetActive(false);

        Allocation a = await RelayService.Instance.CreateAllocationAsync(MaxPlayers);
        var joinCode = await RelayService.Instance.GetJoinCodeAsync(a.AllocationId);
        Debug.Log(joinCode);

        _transport.SetHostRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData);

        var hostStarted = NetworkManager.Singleton.StartHost();
        Debug.Log($"Host Started");
        _buttons.SetActive(true);
		Debug.Log("Game Created");
	}

    public async void JoinGame()
    {
        _buttons.SetActive(false);

        JoinAllocation a = await RelayService.Instance.JoinAllocationAsync(_joinInput.text);

        _transport.SetClientRelayData(a.RelayServer.IpV4, (ushort)a.RelayServer.Port, a.AllocationIdBytes, a.Key, a.ConnectionData, a.HostConnectionData);

        NetworkManager.Singleton.StartClient();
    }
}
