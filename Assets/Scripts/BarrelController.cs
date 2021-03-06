using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BarrelController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public bool IgnoreLocation = false;
    public AudioClip breakSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "JakeScene")
        {
            if (transform.position.y <= 4.5f && transform.position.y >= 4.05f || transform.position.y <= 0.5f &&
                transform.position.y >= -0.5f || transform.position.y <= -3.5f && transform.position.y >= -4.5f)
            {
                rb.velocity = new Vector2(-speed, -2.0f);
            }

            if (transform.position.y <= 2.5f && transform.position.y >= 1.5f ||
                transform.position.y <= -1.5f && transform.position.y >= -2.5f)
            {
                rb.velocity = new Vector2(speed, -2.0f);
            }

            if (transform.position.y <= -6)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            LifeScript.health -= 1;
            AudioSource.PlayClipAtPoint(collider.GetComponent<PlayerController>().playerSounds[0], Vector2.zero);
        }
    }

    void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(breakSound, Vector2.zero);
    }
}
