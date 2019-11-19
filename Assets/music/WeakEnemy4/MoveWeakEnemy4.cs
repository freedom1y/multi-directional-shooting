using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWeakEnemy4 : MonoBehaviour
{
    //プレイヤーの座標取得
    private Transform _playerPos;
    //移動スピード
    [SerializeField]private float _speed;
    //距離
    private Vector3 range;

    [SerializeField]
    public GameObject _startFade;

    // Start is called before the first frame update
    void Start()
    {
        //player取得
        _playerPos = GameObject.Find("Player").transform;
        //距離計算
        range = _playerPos.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_startFade.GetComponent<StartFadeIn>()._IsMoveEnemy == true)
        {
            Debug.Log("a");
            //距離計算
            range = _playerPos.position - transform.position;
            //移動
            transform.position += range.normalized * _speed;
        }
    }
}
