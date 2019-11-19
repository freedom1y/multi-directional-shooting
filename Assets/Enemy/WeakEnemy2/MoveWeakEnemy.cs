using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWeakEnemy : MonoBehaviour
{
    Renderer targetRenderer; // 判定したいオブジェクトのrendererへの参照

    //プレイヤーの座標取得
    private Transform _playerPos;
    //移動スピード
    [SerializeField]private float _speed;
    //距離
    private Vector3 range;
    //テンプエネミーポス
    private Vector3 _tempEnemyPos;
    //テンププレイヤーポス
    private Vector3 _tempPlayerPos;
    private bool _renderFlag;
    [SerializeField]
    public GameObject _startFade;


    void Start()
    {
        targetRenderer = GetComponent<Renderer>();
        //player取得
        _playerPos = GameObject.Find("Player").transform;
        //距離計算
        range = _playerPos.position - transform.position;
        _renderFlag = false;
    }

    void Update()
    { 
        if (_startFade.GetComponent<StartFadeIn>()._IsMoveEnemy == true)
        {
      
            if (targetRenderer.isVisible)
            {
                if (_renderFlag == false)
                {
                    _tempEnemyPos = transform.position;
                    _tempPlayerPos = _playerPos.transform.position;
                    _renderFlag = true;
                }
            }
            else
            {
                if (_renderFlag == false)
                    transform.position += range.normalized * _speed;
            }

            if (_renderFlag == true)
            {
                if (_tempPlayerPos.x <= _tempEnemyPos.x)
                {
                    if (_tempPlayerPos.y <= _tempEnemyPos.y)
                    {
                        transform.Translate(-0.03f, -0.02f, 0.00f);
                    }
                }
                if (_tempPlayerPos.x <= _tempEnemyPos.x)
                {
                    if (_tempPlayerPos.y >= _tempEnemyPos.y)
                    {
                        transform.Translate(-0.03f, 0.02f, 0.00f);
                    }
                }

                if (_tempPlayerPos.x >= _tempEnemyPos.x)
                {
                    if (_tempPlayerPos.y <= _tempEnemyPos.y)
                    {
                        transform.Translate(0.03f, -0.02f, 0.00f);
                    }
                }

                if (_tempPlayerPos.x >= _tempEnemyPos.x)
                {
                    if (_tempPlayerPos.y >= _tempEnemyPos.y)
                    {
                        transform.Translate(0.03f, 0.02f, 0.00f);
                    }
                }

            }
        }
    }
}
