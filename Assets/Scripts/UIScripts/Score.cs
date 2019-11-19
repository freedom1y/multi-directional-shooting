using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    public static int score;
    // Start is called before the first frame update
    void Start()
    {
        score = Score.getScore();
    }


    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void addScore(int s)
    {
        score += s;
    }

    public static void scoreZero()
    {
        score = 0;
    }

    public static int getScore()
    {
        return score;
    }

}
