using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public Enemy[] enemyPrefabs; 
    public float interval; 

    private float timer;
    float tempTime;

    private void Update()
    {
        timer += Time.deltaTime;
        tempTime += Time.deltaTime;
        if (timer < interval) return;

    
        timer = 0;
        if (tempTime >= 5.0f)
        {
            // 出現する敵をランダム決定
            var enemyIndex = Random.Range(0, enemyPrefabs.Length);

        // 敵のプレハブを配列から取得
        var enemyPrefab = enemyPrefabs[enemyIndex];

        var enemy = Instantiate(enemyPrefab);

      
            // 敵を画面外のどの位置に出現させるかランダムに決定する
            var respawnType = (RESPAWN_TYPE)Random.Range(
                0, (int)RESPAWN_TYPE.SIZE);

            // 敵を初期化
            enemy.Init(respawnType);
        }
    }
}