using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private readonly float speed = 5.0f;
    private readonly float jumpForce = 15000.0f;

    private float horizontalMovement = 0.0f;
    public bool isGrounded = false;

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
    }

    public void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Block")
        {
            isGrounded = true;
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
        Vector2 MovementDir = new Vector2(horizontalMovement * speed * 7f, rb.velocity.y);
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
}
