using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWeakEnemy1 : MonoBehaviour
{
    //プレイヤーの座標取得

    private Transform playerPos;
    //移動スピード
    [SerializeField]private float speed;
    //距離
    private Vector3 range;

    [SerializeField]
    public GameObject _startFade;

    void Start()
    {
        //player取得
        playerPos = GameObject.Find("Player").transform;
        //距離計算
        range = playerPos.position - transform.position;
    }


    void Update()
    {
        if (_startFade.GetComponent<StartFadeIn>()._IsMoveEnemy == true)
        {
            //移動
            transform.position += range.normalized * speed;
        }
    }
}
