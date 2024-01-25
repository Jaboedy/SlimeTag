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
    [SerializeField] GameObject NameInput;
    public string pName;

    private void Update()
    {

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("ClaytonsDevScene");
    }

    public void GetPlayerName(string enteredName)
    {
        pName = enteredName;
        Debug.Log("Entered Name: " +  pName);
    }
}
