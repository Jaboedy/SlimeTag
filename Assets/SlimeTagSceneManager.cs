using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SlimeTagSceneManager : NetworkBehaviour
{
    private string JoinCode;
    private NetworkVariable<int> currentMapIndex = new NetworkVariable<int>(0);
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

    public NetworkVariable<int> getCurrentMapIndex() {  return currentMapIndex; }
    public void setCurrentMapIndex(int newIndex)
    {
        currentMapIndex.Value = newIndex;
    }
}
