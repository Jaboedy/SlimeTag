using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class PlayerScoreManager : MonoBehaviour
{
    public string enteredName;
    public int TotaledScore;
    void Start()
    {
        TotaledScore = 0;
        /*if (!GetComponent<MainMenuFunctions>().pName.IsNullOrEmpty())
        {
            //enteredName = GetComponent<MainMenuFunctions>().pName;
        }*/
    }
}