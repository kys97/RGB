using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private int NavDistance;
    [SerializeField] private int AttackNum;
    [SerializeField] private float AttackTime;
    [SerializeField] private float AttackGap;
    [SerializeField] private MushBullet BulletPrefab;

    private GameObject player;

    //private MushBullet copy;
    private float distance;
    private int attack_count = 0;
    private int PlayerDir = 0;
    [SerializeField] private int hp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        distance = 100;
        StartCoroutine("Fire");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (hp == 0)
        {
            Destroy(gameObject);
        }

        if (player.transform.position.x < transform.position.x)
        {
            PlayerDir = -1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            PlayerDir = 1;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "s_Bullet")
        {
            hp = 0;
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            hp -= 1;
        }
    }

    IEnumerator Fire()
    {
        while (true)
        {
            if (attack_count == 2)
                attack_count = 0;

            if (distance < NavDistance)
            {
                while (attack_count < AttackNum)
                {
                    MushBullet copy = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                    copy.dir = PlayerDir;
                    copy.FirePosition = transform.position;
                    attack_count++;
                    yield return new WaitForSeconds(AttackGap);
                }
            }

            yield return new WaitForSeconds(AttackTime);
        }
    }
}
