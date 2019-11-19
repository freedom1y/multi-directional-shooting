using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{

    [SerializeField]private int hp = 50;
    private float speed = 0.1f;
    private int moveControl = 0;
    [SerializeField]private GameObject bullet;
    bool attackContorol = true;
    public int bossPower;
    public GameObject explosionPrefab;
    GameObject scoreText; 
    Score script;

    private AudioSource audioSource;
    GameObject player; 
    PlayerController playerScript; 

    public AudioClip deathClip;
    public AudioClip bossDeathClip;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText");
        script = scoreText.GetComponent<Score>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.hp > 0)
        {
            Move();
            if (attackContorol) StartCoroutine(Attack());
        }
    }

    void Move()
    {
        Vector3 BossPosition = this.transform.position;
        //if (BossPosition.x == 4.0f || BossPosition.x == -4.0f || BossPosition.y == 3.0f || BossPosition.y == -3.0f)
        //{
        //        moveControl++;
        //}
        int moveControlMod4 = moveControl % 4;
        switch (moveControlMod4)
        {
            case 0:
                if (BossPosition.x < -4.0f) moveControl++;
                this.transform.Translate(-1.0f * speed , 0, 0, Space.World);
                break;
            case 1:
                if (BossPosition.y < -3.0f) moveControl++;
                this.transform.Translate(0, -1.0f * speed, 0, Space.World);
                break;
            case 2:
                if (BossPosition.x > 4.0f) moveControl++;
                this.transform.Translate(1.0f * speed, 0, 0, Space.World);
                break;
            case 3:
                if (BossPosition.y > 3.0f) moveControl++;
                this.transform.Translate(0, 1.0f * speed, 0, Space.World);
                break;
            default:
                break;
        }
    }

    IEnumerator Attack()
    {
        Vector3 bulletPosition = this.transform.position;
        Instantiate(bullet,bulletPosition, Quaternion.identity);
        attackContorol = false;
        yield return new WaitForSeconds(1.0f);
        attackContorol = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "playerBullet")
        {
            //Debug.Log("aaa");
            Destroy(col.gameObject);
            StartCoroutine(hitDamage());
            Damage(1);
            audioSource.PlayOneShot(deathClip);
        }

        if(this.hp <= 0)
        {
            StartCoroutine(gameClear(this.gameObject));
        }
    }

    IEnumerator hitDamage()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.color = Color.white;
    }


    public int getHp()
    {
        return this.hp;
    }

    public void Damage(int damage)
    {
        this.hp -= damage;
    }

    IEnumerator gameClear(GameObject Boss)
    {
        //Destroy(Boss);
        //var audioSource = FindObjectOfType<AudioSource>();
        GameObject explosion = Instantiate(
            explosionPrefab,
            new Vector3(Boss.transform.localPosition.x + 0.15f,
                        Boss.transform.localPosition.y,
                        Boss.transform.localPosition.z),
            Quaternion.identity);


        audioSource.PlayOneShot(bossDeathClip);
        yield return new WaitForSeconds(6f);
        Destroy(Boss);
        Destroy(explosion);
        script.addScore(150 * playerScript.hp);
        SceneManager.LoadScene("ResultScene");
    }
}
