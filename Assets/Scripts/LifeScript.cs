using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LifeScript : MonoBehaviour
{
    public GameObject heart1, heart2, heart3;
    public static int health;
    public PlayerController player;

    void Start()
    {
        health = 3;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
    }

    void Update()
    {
        if (health > 3)
        {
            health = 3;
        }

        switch (health)
        {
            case 3:
                {
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(true);
                    break;
                }
            case 2:
                {
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(true);
                    heart3.gameObject.SetActive(false);
                    break;
                }
            case 1:
                {
                    heart1.gameObject.SetActive(true);
                    heart2.gameObject.SetActive(false);
                    heart3.gameObject.SetActive(false);
                    break;
                }
            case 0:
                {
                    heart1.gameObject.SetActive(false);
                    heart2.gameObject.SetActive(false);
                    heart3.gameObject.SetActive(false);

                    if (player != null)
                    {
                        AudioClip audioClip = player.GetComponent<PlayerController>().playerSounds[1];
                        AudioSource.PlayClipAtPoint(audioClip, Vector2.zero);
                        SceneManager.LoadScene("JakeScene", LoadSceneMode.Single);
                    }
                    break;
                }
        }
    }
}
