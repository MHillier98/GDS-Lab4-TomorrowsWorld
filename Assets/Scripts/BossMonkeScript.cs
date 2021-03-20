using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonkeScript : MonoBehaviour
{
    public GameObject Barrel;
    public float ThrowSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ThrowBarrel", 3.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ThrowBarrel()
    {
        Vector2 BarrelPos = transform.position;

        GameObject BarrelToThrow = Instantiate(Barrel, BarrelPos, Quaternion.identity);
        BarrelToThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(ThrowSpeed, 0);
       
    }
    
}
