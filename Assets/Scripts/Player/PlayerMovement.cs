using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public LayerMask groudLayer;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    private Animator playerAnimator;

    [SerializeField] private GameObject rightThresh;
    [SerializeField] private GameObject leftThresh;
    [SerializeField] private GameObject botThresh;

    [SerializeField] private GameObject respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f) 
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        ControlAnim();
        
        if (transform.position.y < botThresh.transform.position.y)
        {
            //yield return new WaitForSeconds(2.5f);
            transform.position = new Vector3(respawnPoint.transform.position.x, respawnPoint.transform.position.y);
        }

        if (transform.position.x < leftThresh.transform.position.x)
        {
            transform.position = new Vector3(rightThresh.transform.position.x, transform.position.y);
        }

        if (transform.position.x > rightThresh.transform.position.x)
        {
            transform.position = new Vector3(leftThresh.transform.position.x, transform.position.y);
        }


    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
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
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void ControlAnim()
    {
        if (rb.velocity.y > 1)
        {
            playerAnimator.SetTrigger("jump");
        }
        else
        {
            if (horizontal > 0f || horizontal < 0f)
            {
                playerAnimator.SetTrigger("run");
            }
            else if (horizontal == 0f)
            {
                playerAnimator.SetTrigger("idle");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            playerAnimator.SetTrigger("hit");
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
    }
}
