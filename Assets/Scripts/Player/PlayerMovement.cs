using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour
{
    public LayerMask groudLayer;
    public float fallSpeed = 0.1f;

	private Rigidbody2D rb;

	private float horizontal;
    
    private bool isFacingRight = false;

    [SerializeField] private GameObject scoreManager;

    [SerializeField] private Material infectedMat;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private GameObject rightThresh;
    [SerializeField] private GameObject leftThresh;
    [SerializeField] private GameObject botThresh;

    [SerializeField] private GameObject respawnPoint;

    [SerializeField] private List<Material> materials;

    private SlimeTagSceneManager gameManager;

    public NetworkVariable<bool> isInfected = new NetworkVariable<bool>(false);

    [SerializeField] private bool isMovable = true;

    // Use instead of awake/start for network
    public override void OnNetworkSpawn()
    {
        gameManager = FindObjectOfType<SlimeTagSceneManager>();
        gameManager.AddPlayer(gameObject);
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().material = materials[((int)OwnerClientId)];

    }

    // Update is called once per frame
    void Update()
    {
        if (isInfected.Value)
        {
            gameObject.GetComponent<SpriteRenderer>().material = infectedMat;
        }
        if (isMovable)
        {
            if (Input.GetKey(KeyCode.A))
            {
                horizontal = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                horizontal = 1;
            }
            if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                horizontal = 0;
            }
        }
        /*if (!IsOwner)
        {
            return;
        }*/

        /*float yVel;
        if (isTouchingGround)
        {
            yVel = 0;
            var hit = Physics2D.Raycast(transform.position, -transform.up);
            if (hit)
            {
                //Debug.Log(hit.transform.gameObject.name);
            }
            
        }
        else
        {
            yVel = rb.velocity.y - fallSpeed;
        }
        rb.velocity = new Vector2(horizontal * speed, yVel);*/


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
    /*public void Jump(InputAction.CallbackContext context)
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
    }*/

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void ControlAnim()
    {
        if (GetComponent<PlatformChaser2DModified>().isAirborne)
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
        if (isInfected.Value && collision.gameObject.layer == 3 && collision.GetComponent<PlayerMovement>().isInfected.Value == false)
        {
            collision.GetComponent<PlayerMovement>().BecomeInfected();
            gameObject.GetComponent<PlayerScoreManager>().TotaledScore += 2;
        }
    }
    public void BecomeInfected()
    {
        isInfected.Value = true;
        scoreManager.GetComponent<ScoreManager>().infectedPlayers.Add(gameObject);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.5f);
    }
}
