using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text levelText;

    void Update()
    {
        levelText.text = PlayerController.level.ToString();
    }
}
