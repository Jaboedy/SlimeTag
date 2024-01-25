using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ReadyZone : MonoBehaviour
{
    SlimeTagRelay relay;
    List<GameObject> playersInZone = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        relay = FindObjectOfType<SlimeTagRelay>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playersInZone.Count == relay.GetConnectedPlayers().Count)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("ClaytonsDevScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("COLLISION HERE");
		if (collision != null)
        {
            Debug.Log("Collision not null");
            if (collision.gameObject.layer == 3 && !playersInZone.Contains(collision.gameObject))
            {
                Debug.Log("Player Layer");
                playersInZone.Add(collision.gameObject);
            }
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision != null)
        {
            if (collision.gameObject.layer == 3)
            {
                playersInZone.Remove(collision.gameObject);
            }
        }
	}
}
