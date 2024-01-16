using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask groudLayer;
    public float fallSpeed = 0.1f;

	private Rigidbody2D rb;

	private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f;
    private bool isFacingRight = false;
    private bool isTouchingGround = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float yVel;
        if (isTouchingGround)
        {
            yVel = 0;
            var hit = Physics2D.Raycast(transform.position, -transform.up);
            if (hit)
            {
                Debug.Log(hit.transform.gameObject.name);
            }
            
        }
        else
        {
            yVel = rb.velocity.y - fallSpeed;
        }
        rb.velocity = new Vector2(horizontal * speed, yVel);


        if (!isFacingRight && horizontal > 0f) 
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && true)
        {
            var up = transform.up * jumpingPower;
            rb.velocity = new Vector2(rb.velocity.x + up.x, rb.velocity.y + up.y);
            isTouchingGround = false;
        }
        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log("Entered Collision");
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Collision with env layer");
			isTouchingGround = true;
		}
	}
}
