using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerGravityControl : MonoBehaviour
{
    private Vector3 direction;
    private RaycastHit2D ray;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = transform.TransformDirection(Vector3.down) * 1;
        ray = Physics2D.Raycast(transform.position, direction, 1f, 6);
        StickToSurface();
    }
    
    private void StickToSurface()
    {
        Debug.Log(ray.collider.gameObject.name);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
