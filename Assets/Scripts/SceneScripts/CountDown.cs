using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    public Text _textCountdown;
    [SerializeField]
    public Image _imageMask;

    private int countStart;
    [SerializeField]
    public StartFadeIn _endFadeIn;
    [SerializeField]
    public StartFadeIn _isMooveEnemy;
    int temp = 0;

    void Start()
    {
        _textCountdown.text = "";
        countStart = 0;
        _isMooveEnemy._IsMoveEnemy = false;
    }

    void Update()
    {
        //Debug.Log(countStart);
        Debug.Log(countStart);
        if ( countStart == 0)
        {
            _endFadeIn._endFadeIn = 2;
            countStart = 1;
            temp = 1;
        }

        if(countStart == 1 && temp == 1)
        {
            temp = 2;
            StartCoroutine(CountdownCoroutine());
        }

        if (countStart == 2)
        {
           _isMooveEnemy._IsMoveEnemy = true;
        }
    }

    IEnumerator CountdownCoroutine()
    {
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "3";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "2";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "1";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "GO!";
        countStart = 2;
        yield return new WaitForSeconds(1.0f);
        

        _textCountdown.text = "";
        _textCountdown.gameObject.SetActive(false);
        _imageMask.gameObject.SetActive(false);
    }

}
