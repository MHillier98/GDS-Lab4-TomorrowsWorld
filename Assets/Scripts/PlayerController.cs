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
    private bool isTouchingLadder = false;

    public bool hasHammer = false;
    public Animator anim;
    public Transform attackPoint;
    private float attackRange = 0.9f;
    public LayerMask enemyLayers;
    public Transform HammerPickup;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Jump();
        Attack();
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

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasHammer == true)
        {
            anim.SetTrigger("Attack");
            rb.velocity = new Vector2(0f, 0f);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Destroy(enemy.gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.G) && hasHammer == true)
        {
            hasHammer = false;
            Instantiate(HammerPickup, new Vector2((attackPoint.position.x - 1f), attackPoint.position.y), Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hammer")
        {
            hasHammer = true;
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

        if(isTouchingLadder){
            verticalMovement = Input.GetAxis("Vertical");
            
            /*if(!isGrounded){
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }else{
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }*/
        }else{
            verticalMovement = 0;
           // rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        Vector2 MovementDir = new Vector2(horizontalMovement * speed * 6f, verticalMovement * speed * 10f);
        rb.AddForce(MovementDir);

        if (horizontalMovement > 0)
        {
            sprite.flipX = false;
        }
        else if (horizontalMovement < 0)
        {
            sprite.flipX = true;
        }
        if (MovementDir == new Vector2(0f, 0f))
        {
            anim.SetBool("isMoving", false);
        }
        else anim.SetBool("isMoving", true);

    }

}
