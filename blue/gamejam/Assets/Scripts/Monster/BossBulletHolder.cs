using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletHolder : MonoBehaviour
{
    public GameObject bossBullet;
    public Transform playerTransform;

    public Transform firePos;
    Queue<GameObject> bullets = new Queue<GameObject>();
    public float speed;
    Quaternion startPos;
    bool isFiring = false;
    public FireMode fireMode;
    private void Awake()
    {
        for(int i = 0; i < 30; i++)
        {
            CreateBullet();
        }
    }
    private void Start()
    {
        startPos = transform.rotation;
        fireMode = FireMode.none;
    }
    Coroutine fire;
    private void Update()
    {
        if(fireMode != FireMode.none && !isFiring)
        {
            isFiring = true;
            fire = StartCoroutine(Attack());
        }
        else if (fireMode == FireMode.none && isFiring)
        {
            isFiring = false;
            StopCoroutine(fire);
        }
        switch (fireMode)
        {
            case FireMode.circle:
                transform.Rotate(new Vector3(0, 0, 1));
                break;
            case FireMode.swing:
                Quaternion a = startPos;
                a.z += 1f * (1f * Mathf.Sin(Time.time * 2f));
                transform.rotation = a;
                break;
        }
    }
    public void ReturnBullet(GameObject bullet)
    {
        bullets.Enqueue(bullet);
        bullet.SetActive(false);
    }
    void CreateBullet()
    {
        GameObject newB_bullet = Instantiate(bossBullet);
        bullets.Enqueue(newB_bullet);
        newB_bullet.SetActive(false);
    }
    void Fire()
    {
        if (bullets.Count == 0)
            CreateBullet();
        GameObject bullet = bullets.Dequeue();
        bullet.SetActive(true);
        bullet.transform.position = transform.position;
        bullet.GetComponent<BossBullet>().direction = (firePos.position - transform.position).normalized;
        
    }

    
    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Fire();
        }
    }

    public enum FireMode
    {
        circle,
        swing,
        none
    }
}
