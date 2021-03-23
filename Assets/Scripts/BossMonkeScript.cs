using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonkeScript : MonoBehaviour
{
    public GameObject Barrel;
    public AudioClip[] bossMonkeySounds;
    public float ThrowSpeed = 3.0f;
    private Animator anim;
    private AudioSource audioSource;

    void Start()
    {
        if (Barrel)
        {
            InvokeRepeating("ThrowBarrel", 3.0f, 3.0f);
            anim = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>(); //0 = Boss Monkey Death, 1 = Boss Monkey Moving to Next Level, 2 = Boss Monkey Throw
        }
        else
        {
            return;
        }
    }

    void ThrowBarrel()
    {
        anim.SetBool("Throwing", true);
        Vector2 BarrelPos = transform.position;
        AudioSource.PlayClipAtPoint(bossMonkeySounds[2], Vector2.zero, 0.5f);

        GameObject BarrelToThrow = Instantiate(Barrel, BarrelPos, Quaternion.identity);
        BarrelToThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(ThrowSpeed, 0);

        StartCoroutine(EndThrow());
    }

    IEnumerator EndThrow()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("Throwing", false);
    }
}
