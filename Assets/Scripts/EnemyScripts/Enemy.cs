using System.Collections;
using UnityEngine;

// 出現位置の種類
public enum RESPAWN_TYPE
{
    UP,  
    DOWN,
    RIGHT,
    LEFT, 
    SIZE
}

public class Enemy : MonoBehaviour
{
    public Vector2 respawnPosInside; // 敵の出現位置（内側）
    public Vector2 respawnPosOutside; // 敵の出現位置（外側）

    public float speed; 
    public int hpMax; 
    public int exp; 
    public int damage; 

    private int hp; 
    private Vector3 direction; // 進行方向

    GameObject scoreText; 
    Score script; 

    public GameObject explosionPrefab;

    private int random;

    GameObject player; 
    PlayerController playerScript; 

    public AudioClip deathClip;


    private void Start()
    {
        hp = hpMax;
        scoreText = GameObject.Find("ScoreText");
        script = scoreText.GetComponent<Score>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
    }


    private void Update()
    {
        //transform.localPosition += direction * speed;
    }

    // 敵の出現パターン
    public void Init(RESPAWN_TYPE respawnType)
    {
        var pos = Vector3.zero;

        switch (respawnType)
        {
            case RESPAWN_TYPE.UP:
                pos.x = Random.Range(
                    -respawnPosInside.x, respawnPosInside.x);
                pos.y = respawnPosOutside.y;
                //direction = Vector2.up;
                break;

            case RESPAWN_TYPE.DOWN:
                pos.x = Random.Range(
                    -respawnPosInside.x, respawnPosInside.x);
                pos.y = -respawnPosOutside.y;
                //direction = Vector2.up;
                break;

            case RESPAWN_TYPE.RIGHT:
                pos.x = respawnPosOutside.x;
                pos.y = Random.Range(
                    -respawnPosInside.y, respawnPosInside.y);
                //direction = Vector2.left;
                break;

            case RESPAWN_TYPE.LEFT:
                pos.x = -respawnPosOutside.x;
                pos.y = Random.Range(
                    -respawnPosInside.y, respawnPosInside.y);
                //direction = Vector2.right;
                break;
        }

        // 位置反映
        transform.localPosition = pos;
    }
    private void UpDate()
    {
        if (this.transform.position.x < 7 || transform.position.x > -7)
            return;

        if (this.transform.position.y < 7 || this.transform.position.y > -7)
            return;
        Destroy(this.gameObject);
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            var player = col.GetComponent<PlayerController>();
            player.Damage(damage);
            Destroy(this.gameObject);
            //return;
        }


        if (col.gameObject.tag == "playerBullet")
        {

            Destroy(col.gameObject);

            hp--;
    
            if (0 < hp) return;
            //FindObjectOfType<Score>().addPoint(10);
            script.addScore(10);

            playerScript.addExp(exp);

            GameObject explosion = Instantiate(
                explosionPrefab,
                col.transform.localPosition,
                Quaternion.identity);

            //Debug.Log("ex:"+explosion);
            StartCoroutine(destroyExplosion(explosion,this.gameObject));

            var audioSource = FindObjectOfType<AudioSource>();
            audioSource.PlayOneShot(deathClip);
        }
    }

    IEnumerator destroyExplosion(GameObject a,GameObject b)
    {

        yield return new WaitForSeconds(0.2f);
        //Debug.Log("hdewhoi");
        Destroy(a);
        Destroy(b);
    }


}


