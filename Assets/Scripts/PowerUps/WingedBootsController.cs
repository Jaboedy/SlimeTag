using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingedBootsController : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlatformChaser2DModified>().StartSpeedBoost();
            Destroy(gameObject);
        }
    }
}
