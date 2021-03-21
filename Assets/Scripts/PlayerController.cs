using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField]
    private BoxCollider2D Box;

    private readonly float speed = 5.0f;
    private readonly float jumpForce = 15000.0f;

    private float horizontalMovement = 0.0f;
    public float verticalMovement = 0.0f;
    public bool isGrounded = false;
    public bool isTouchingLadder = false;
    public bool goingDown = false;

    public bool hasHammer = false;
    public Animator anim;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Jump();
       
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Block")
        {
            isGrounded = true;
        }
        if (collider.tag == "Ladder")
        {
            isTouchingLadder = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Ladder"){
            isTouchingLadder = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hammer")
        {
            hasHammer = true;
            anim.SetBool("hasHammer", true);
            Destroy(collision.gameObject);
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * jumpForce);
            }

            isGrounded = false;
        }
    }

    private void Move()
    {
        horizontalMovement = Input.GetAxis("Horizontal");

        if (isTouchingLadder)
        {
            verticalMovement = Input.GetAxis("Vertical");
            if (verticalMovement != 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        else
        {
            verticalMovement = 0;
        }
        if (!isTouchingLadder || verticalMovement == 0)
        {
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }

        if (verticalMovement >= 0)
        {
            Vector2 MovementDir = new Vector2(horizontalMovement * speed * 7f, verticalMovement * speed * 10f);
            rb.AddForce(MovementDir);
        }
        if(verticalMovement < 0 && isTouchingLadder)
        {
            //need some code that will allow the player to move down the ladder
            //I tried to implement some collision ignoring things but nothing seemed to work properly
            goingDown = true;
        }
        else
        {
            goingDown = false;
        }

        if (horizontalMovement > 0)
        {
            sprite.flipX = false;
        }
        else if (horizontalMovement < 0)
        {
            sprite.flipX = true;
        }     
    }
}
