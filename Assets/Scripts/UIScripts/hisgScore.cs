using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hisgScore : MonoBehaviour
{
    // Start is called before the first frame update
    public Text highScoreText;
    public static int highScore = 0;
    void Start()
    {
        highScore = resultScore.getHigh();
    }

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = highScore.ToString();
    }
}
