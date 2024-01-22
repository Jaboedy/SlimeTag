using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTagSceneManager : MonoBehaviour
{
    private string JoinCode;
    // Start is called before the first frame update
    private void Start()
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
}
