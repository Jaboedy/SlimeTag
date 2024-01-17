using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class MainMenuFunctions : MonoBehaviour
{
    private string hostingCode;
    private string hostingPlayerName;

    [SerializeField] private GameObject hostCodeDisplay;
    private TextMeshProUGUI hostCodeDisplaytext;

    [SerializeField] private GameObject playerNameDisplay;
    private TextMeshProUGUI playerNameDisplaytext;

    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;

    void Start()
    {
       hostCodeDisplaytext = hostCodeDisplay.GetComponent<TextMeshProUGUI>();
       playerNameDisplaytext = playerNameDisplay.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        hostCodeDisplaytext.text = "Hosting Code: "; //+ put hosting code here however we get it
        playerNameDisplaytext.text = hostingPlayerName;
    }



    public void GetHostCode(string code)
    {
        hostingCode = code;
    }
    public void GetHostName(string name)
    {
        hostingPlayerName = name;
    }

    public void EnableDisableHostButton(Button button)
    {
        if (hostingPlayerName.IsNullOrEmpty() == false)
        {
            button.interactable = true;
        } else { button.interactable = false;
        }
    }
    public void EnableDisableJoinButton(Button button)
    {
        if (hostingPlayerName.IsNullOrEmpty() == false && hostingCode.IsNullOrEmpty() == false)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("ClaytonsDevScene");
    }
}
