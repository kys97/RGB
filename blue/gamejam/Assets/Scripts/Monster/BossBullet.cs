using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float rotateSpeed;
    BossBulletHolder bossBullets;
    private void Start()
    {
        bossBullets = GameObject.Find("Boss").GetComponentInChildren<BossBulletHolder>();
    }
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (!GetComponent<Renderer>().isVisible)
        {
            bossBullets.ReturnBullet(gameObject);
        }
        //transform.Rotate(new Vector3(0, 0, -1) * rotateSpeed);
    }
    private void OnEnable()
    {
         //direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Bullet_Enemy" && collision.tag != "Boss")
        {
            bossBullets.ReturnBullet(gameObject);
        }
    }
}
