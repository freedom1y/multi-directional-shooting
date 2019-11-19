
﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public ShotController shotPrefab;
    public float shotSpeed;
    public float shotAngleRange;
    public float shotTimer;
    public int shotCount;
    public float shotInterval;
    //public int shotPower;

    public int hpMax;
    public int hp;

    public static int multiCount; // 必殺技を打てる数

    private int exp = 0;

    private float timeElapsed;
    public int nextExpBase; 
    public int nextExpInterval; 
    public static int level;
    public int sumExp;
    public int sumLevelExp;
    public int needExp; // 次のレベルに必要な経験値

    public static PlayerController instance;

    Slider _slider;

    Vector3 mousePosOld;

    bool superArmer = false;

    Slider _slider2;

    public AudioClip levelUpClip;
    public AudioClip damageClip;

    private bool isDoubleTapStart; //タップ認識中のフラグ
    private float doubleTapTime; //タップ開始からの累積時間

    Score script;

    private void Awake()
    {
        instance = this;

        _slider = GameObject.Find("Slider").GetComponent<Slider>();
        hp = hpMax;
        mousePosOld = new Vector3(0, 0, 0);
        hpSliderMove();

        _slider2 = GameObject.Find("Slider2").GetComponent<Slider>();
        mousePosOld = new Vector3(0,0,0);

        level = 1;
        sumExp = 0;
        sumLevelExp = 0;
        needExp = GetNeedExp(1); // 次のレベルに必要な経験値

        multiCount = 0;

        isDoubleTapStart = false;
        doubleTapTime = 0.0f;

        script = GameObject.Find("ScoreText").GetComponent<Score>();
    }

    private void Update()
    {
        //var h = Input.GetAxis("Horizontal");
        //var v = Input.GetAxis("Vertical");

        /*if (!(Input.GetMouseButton(0)))
        {
            var velocity = new Vector3(h, v) * speed;
            transform.localPosition += velocity;
        }*/

        //var velocity = new Vector3(h, v) * speed;
        //transform.localPosition += velocity;


        transform.localPosition = Utils.ClampPosition(transform.localPosition);


        // スクリーン座標
        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // プレイヤーから見たマウスカーソルの方向
        var direction = Input.mousePosition - screenPos;

        // マウスカーソルが存在する方向の角度
        var angle = Utils.GetAngle(Vector3.zero, direction);

        // マウスカーソルの方向を見る
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);



        if (mousePos != mousePosOld)
        {
            mousePosOld = mousePos;
            timeElapsed = 0.0f;
        }
        else
        {
            timeElapsed += Time.deltaTime;
        }



        var velocity = new Vector3(direction.x, direction.y).normalized * speed;
        //Debug.Log("aa:" + (transform.localPosition - velocity));
        if (timeElapsed > 0.1f && (mousePos.x - 0.2 < transform.localPosition.x) && (mousePos.x + 0.2 > transform.localPosition.x))
        {

        }
        else
        {
            transform.localPosition += velocity;
        }
        shotTimer += Time.deltaTime;

        if (shotTimer < shotInterval) return;

        shotTimer = 0;



        // ダブルタップ判定(反応悪い)
        if (isDoubleTapStart)
        {
            doubleTapTime += Time.deltaTime;
            if (doubleTapTime < 0.7f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // 必殺技を発射
                    MultiShot();
                    isDoubleTapStart = false;
                    doubleTapTime = 0.0f;
                }
            }
            else
            {
                isDoubleTapStart = false;
                doubleTapTime = 0.0f;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDoubleTapStart = true;
            }

        }


        if (Input.GetMouseButton(0))
        {
            // 弾を発射
            ShootNWay(angle, shotAngleRange, shotSpeed, shotCount);
        }
    }

    private void ShootNWay(
        float angleBase, float angleRange, float speed, int count)
    {
        var pos = transform.localPosition; // プレイヤーの位置
        var rot = transform.localRotation; // プレイヤーの向き

        if (1 < count)
        {
            for (int i = 0; i < count; ++i)
            {
                var angle = angleBase +
                    angleRange * ((float)i / (count - 1) - 0.5f);

                var shot = Instantiate(shotPrefab, pos, rot);

                // 弾を発射する方向と速さを設定
                shot.Init(angle, speed);
            }
        }
        else if (count == 1)
        {
            // 弾を生成
            var shot = Instantiate(shotPrefab, pos, rot);

            // 発射する方向と速さ
            shot.Init(angleBase, speed);
        }

    }

    public void Damage(int damage)
    {
        if (!superArmer)
        {
            hp -= damage;
            

            var audioSource = FindObjectOfType<AudioSource>();
            audioSource.PlayOneShot(damageClip);

            StartCoroutine(hitDamage());
            if (hp > 0) return;
            else hp = 0;
            SceneManager.LoadScene("ResultScene");
            //gameObject.SetActive(false);
            superArmer = true;

        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        hpSliderMove();
        var boss = col.GetComponent<BossScript>();
        if (col.gameObject.tag == "BossBullet")
        {
            Destroy(col.gameObject);

            //Debug.Log("kita");
            Damage(boss.bossPower);
            return;

        }else if(col.gameObject.tag == "Boss")
        {
            Damage(boss.bossPower);
            return;
        }
    }

    IEnumerator hitDamage()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = Color.white;
        superArmer = false;//ここから入らない
        Debug.Log("supe");
    }

    void hpSliderMove()
    {
        _slider.value = hp;
    }


    void expSliderMove(int exp)
    {
        _slider2.value += exp;
    }

    void expSliderUp()
    {
        _slider2.maxValue = needExp;
        _slider2.value = sumExp - sumLevelExp;
    }



    public void addExp(int exp)
    {
        this.exp += exp;
        sumExp += exp;
        expSliderMove(exp);

        if (this.exp < needExp) return;

        level++;

        // これまでのレベルアップに必要だった経験値
        sumLevelExp += needExp;

        // 次のレベルアップに必要な経験値
        needExp = GetNeedExp(level);

        expSliderUp();
        this.exp = sumExp - sumLevelExp;

        if (level >= 6)
        {
            script.addScore(150 * hp);
            SceneManager.LoadScene("BossScene");
        }
        
        if(level % 2 == 0)
        {
            multiCount++;
        }

        var audioSource = FindObjectOfType<AudioSource>();
        audioSource.PlayOneShot(levelUpClip);
    }


    private int GetNeedExp(int level)
    {
        return nextExpBase +
            nextExpInterval * ((level - 1) * (level - 1));
    }

    private void MultiShot()
    {
        if (multiCount > 0)
        {
            int angleBase = 0;
            int angleRange = 360;
            int count = 28;
            ShootNWay(angleBase, angleRange, 0.15f, count);
            ShootNWay(angleBase, angleRange, 0.2f, count);
            ShootNWay(angleBase, angleRange, 0.25f, count);
            multiCount--;
        }
    }
}