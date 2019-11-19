using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFadeIn : MonoBehaviour
{
    [SerializeField]
    private FadeInOut _fadeScript;
    public int _endFadeIn;

    public bool _IsMoveEnemy;

    // Start is called before the first frame update
    void Start()
    {
        _fadeScript._fadeInFlag = true;
        _endFadeIn = 0;
        _IsMoveEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeScript._fadeInFlag == false && _endFadeIn == 0)
            _endFadeIn = 1;

    }
}
