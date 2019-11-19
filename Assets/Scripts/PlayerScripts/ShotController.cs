using UnityEngine;


public class ShotController : MonoBehaviour
{
    private Vector3 velocity; // 速度

    private void Update()
    {
        transform.localPosition += velocity;
    }

    public void Init(float angle, float speed)
    {
        var direction = Utils.GetDirection(angle);

        velocity = direction * speed;

        // 弾が進行方向を向く
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        Destroy(gameObject, 2);
    }

}