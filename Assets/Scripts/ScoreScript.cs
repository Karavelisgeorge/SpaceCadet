using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreScript : MonoBehaviour {

    public static int scoreValue = 0;
    Text score;

    void Start()
    {
        scoreValue = 0;
        score = GetComponent<Text>();
    }

    void Update()
    {
        score.text = "Score: " + scoreValue;
    }
}
