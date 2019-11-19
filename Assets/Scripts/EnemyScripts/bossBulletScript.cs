using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBulletScript : MonoBehaviour
{
//a

    Vector3 playerPos;
    Vector3 thisPos;
    Vector3 moveDirection;
    private float speed = 0.01f;
    private int power = 2;

    GameObject player; //Unityちゃんそのものが入る変数

    PlayerController playerScript; //UnityChanScriptが入る変数


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerPos = player.transform.position;
        thisPos = this.transform.position;
        playerScript = player.GetComponent<PlayerController>(); //unitychanの中にあるUnityChanScriptを取得して変数に格納する
        //float dx = playerPos.x - this.transform.position.x;
        //float dy = playerPos.y - this.transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        confirm();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("bbb");
            //Destroy(col.gameObject);
            //StartCoroutine(hitDamage());
            playerScript.Damage(power);
            Debug.Log("playerhp:" + playerScript.hp);
        }
    }

    void Move()
    {
        this.transform.Translate((playerPos.x - thisPos.x) * speed, (playerPos.y - thisPos.y) * speed, 0, Space.World);
    }

    void confirm()
    {
        Vector3 thisPos = this.transform.position;
        if (thisPos.x > 6.0f || thisPos.x < -6.0f) Destroy(this.gameObject);
        if (thisPos.y > 4.5f || thisPos.x < -4.5f) Destroy(this.gameObject);
    }
}
