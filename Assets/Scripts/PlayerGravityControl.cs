using UnityEngine;

public class PlayerGravityControl : MonoBehaviour
{
    private Vector3 direction;
    private static RaycastHit2D ray;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction = transform.TransformDirection(Vector3.down);
        ray = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y - 1.1f), direction, 0.2f, 6);
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 0.6f), direction, Color.red);
        //StickToSurface();

    }
    
    private void StickToSurface()
    {
        if (ray.rigidbody.name != null)
        {
            Debug.Log(ray.rigidbody.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
