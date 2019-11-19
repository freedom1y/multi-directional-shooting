using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToGameScene : MonoBehaviour
{
    [SerializeField]
    private FadeInOut _fadeScript;
    private bool _clickFlag;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false; // 縦
        Screen.autorotateToLandscapeLeft = true; // 左
        Screen.autorotateToLandscapeRight = true; // 右
        Screen.autorotateToPortraitUpsideDown = false; // 上下逆

        Score.scoreZero();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _fadeScript._fadeInFlag == false)
        {
            Debug.Log("a");
            _clickFlag = true;
            _fadeScript._fadeOutFlag = true;
        }

        if (_fadeScript._fadeOutFlag == false && _clickFlag == true)
        {
            Debug.Log("b");

            SceneManager.LoadScene("GameScene");
        }
    }
}
