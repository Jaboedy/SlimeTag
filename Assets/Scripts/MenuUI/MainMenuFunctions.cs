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

    public void LoadGame()
    {
        SceneManager.LoadScene("ClaytonsDevScene");
    }
}
