using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ReadyZone : MonoBehaviour
{
    private SlimeTagRelay relay;
    private List<GameObject> playersInZone = new List<GameObject>();
    [SerializeField] private TMP_Text readyText;
    // Start is called before the first frame update
    void Start()
    {
        relay = FindObjectOfType<SlimeTagRelay>();
    }

    // Update is called once per frame
    void Update()
    {
        readyText.text = $"Players Ready: {playersInZone.Count}/{relay.GetConnectedPlayers().Count}";
        if (playersInZone.Count == relay.GetConnectedPlayers().Count)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("ClaytonsDevScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision != null)
        {
            if (collision.gameObject.layer == 3 && !playersInZone.Contains(collision.gameObject))
            {
                playersInZone.Add(collision.gameObject);
            }
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision != null)
        {
            if (collision.gameObject.layer == 3 && playersInZone.Contains(collision.gameObject))
            {
                playersInZone.Remove(collision.gameObject);
            }
        }
	}
}
