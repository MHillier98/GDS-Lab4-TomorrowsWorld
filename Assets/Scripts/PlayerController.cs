using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private readonly float speed = 5.0f;
    private readonly float jumpForce = 15000.0f;

    private float horizontalMovement = 0.0f;
    private float verticalMovement = 0.0f;
    public bool isGrounded = false;
    public bool isTouchingLadder = false;
    public bool isWindy = false;

    public bool hasHammer = false;
    public Animator anim;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        if (isWindy)
        {
            ApplyWindForce();
        }
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Block")
        {
            isGrounded = true;
        }
        else if (collider.tag == "Ladder")
        {
            isTouchingLadder = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ladder")
        {
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
            if (verticalMovement > 0)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }

            //if (!isGrounded)
            //{
            //    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            //}
            //else
            //{
            //    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //}
        }
        else
        {
            verticalMovement = 0;
            // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (!isTouchingLadder)
        {
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }

        Vector2 MovementDir = new Vector2(horizontalMovement * speed * 7f, verticalMovement * speed * 10f);
        rb.AddForce(MovementDir);

        if (horizontalMovement > 0)
        {
            sprite.flipX = false;
        }
        else if (horizontalMovement < 0)
        {
            sprite.flipX = true;
        }
    }

    private void ApplyWindForce()
    {
        rb.AddForce(new Vector2(-14.0f, 0.0f));
    }
}
