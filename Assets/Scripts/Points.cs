using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{

    public static int scoreValue = 4000;
    Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        InvokeRepeating("Minus", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;
    }

    public void Minus()
    {
        if (scoreValue > 0)
        {
            scoreValue -= 25;
        } 
    }
}
