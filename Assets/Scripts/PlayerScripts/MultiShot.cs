using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiShot : MonoBehaviour
{
    public Text multiShotText;
    
    void Update()
    {
        multiShotText.text = PlayerController.multiCount.ToString();
    }

}