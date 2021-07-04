using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    CharacterController2D controller;
    public float bulletSpeed;
    public float rotateSpeed;

    public bool isLookingRight = true;
    private void Start()
    {
        controller = GameObject.Find("Player").GetComponent<CharacterController2D>();
    }
    private void OnEnable()
    {
        isLookingRight = true;
    }
    private void Update()
    {
        //보고있는 방향으로 발사
        if (isLookingRight)
            transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
        else
            transform.position -= Vector3.right * bulletSpeed * Time.deltaTime;
        //화면에서 없어지면 총알 비활성화
        if (!GetComponent<Renderer>().isVisible)
        {
            controller.ReturnBullet(gameObject);
        }
        transform.Rotate(new Vector3(0, 0, -1) * rotateSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag != "Player" && collision.tag != "Confiner")
        {
            if(collision.tag == "Monster")
            {
                PlayerStat.instance.ChangeGage(1);
            }
            if(collision.tag == "Wall")
            {
                collision.gameObject.SetActive(false);
            }
            controller.ReturnBullet(gameObject);
        }
    }
}
