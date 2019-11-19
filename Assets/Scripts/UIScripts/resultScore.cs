using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class resultScore : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update
    public static int resScore;
    public static int highScore;

    void Start()
    {
        resScore = Score.getScore();
        highScore = PlayerPrefs.GetInt("highScore1");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = resScore.ToString();
        if (resScore > highScore)
        {
            PlayerPrefs.SetInt("highScore1", resScore);
            highScore = resScore;
            PlayerPrefs.Save();
        }
        Debug.Log("res:"+resScore + " high:" + highScore);
    }

    public static int getHigh()
    {
        return highScore;
    }
}
