using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PowerUpSpawnPoint : MonoBehaviour
{
    public GameObject[] powerUps;
    private Collider2D detectPowerUp;
    private LayerMask layer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnRandomPowerUp());
        layer = LayerMask.GetMask("Items");
    }

    // Update is called once per frame
    void Update()
    {
        detectPowerUp = Physics2D.OverlapCircle(gameObject.transform.position, 1f, layer);
    }

    private IEnumerator spawnRandomPowerUp()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(Random.Range(25, 50));
            if (detectPowerUp == null) 
            {
                Instantiate(powerUps[Random.Range(0, powerUps.Length)], gameObject.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, 1f);
    }
}
