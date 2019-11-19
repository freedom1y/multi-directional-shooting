using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWeakEnemy3 : MonoBehaviour
{
    Vector3 playerPos;
    float x1;
    float x2;
    
    [SerializeField]
    public GameObject _startFade;
    void Start()
    {
        playerPos = GameObject.Find("Player").transform.position;
        x1 = (Random.insideUnitCircle.x + 0.3f) * 4;
        x2 = (Random.insideUnitCircle.y + 0.3f) * 4;
        
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (_startFade.GetComponent<StartFadeIn>()._IsMoveEnemy == true)
        {
            transform.position = new Vector3(x1, x2, 0) + playerPos;
        }
    }
}
