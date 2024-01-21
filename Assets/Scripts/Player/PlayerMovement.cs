using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    public LayerMask groudLayer;
    public float fallSpeed = 0.1f;

	private Rigidbody2D rb;

	private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f;
    private bool isFacingRight = false;
    private bool isTouchingGround = false;

    private Animator playerAnimator;

    [SerializeField] private GameObject rightThresh;
    [SerializeField] private GameObject leftThresh;
    [SerializeField] private GameObject botThresh;

    [SerializeField] private GameObject respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!IsOwner)
        {
            return;
        }*/

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

    // Makes all players jump on input
    // TODO: FIX THIS SHIT
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

    public void StartSpeedBoost()
    {
        StartCoroutine(SpeedBoost());
    }

    private IEnumerator SpeedBoost()
    {
        speed = 12f;
        yield return new WaitForSeconds(6f);
        speed = 8f;
    }
}
