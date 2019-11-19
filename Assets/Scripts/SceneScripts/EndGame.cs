using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private Score scoreText;
    private int score;

    public void End()
    {
       Application.Quit();
    }
}
