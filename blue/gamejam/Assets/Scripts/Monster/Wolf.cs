using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float DashSpeed;
    [SerializeField] private float BlinkTime;
    [SerializeField] private float NavDistance;
    [SerializeField] private int NavTime;
    [SerializeField] private int hp;
    

    private int dir;
    private int PlayerDir;
    private float time = 0.0f;
    private int cnt = 0;
    private bool MoveOk = true;
    private bool Dash = false;
    private GameObject player;
    private PlayerInteract interact;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        interact = player.GetComponent<PlayerInteract>();
        int start = Random.Range(0, 2);

        if (start == 0) dir = -1;
        else dir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        time += Time.deltaTime;

        if(hp == 0)
        {
            Destroy(gameObject);
        }

        if (Dash)
        {
            if(PlayerDir == -1) GetComponent<SpriteRenderer>().flipX = false;
            else if(PlayerDir == 1) GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(new Vector3(PlayerDir, 0, 0) * DashSpeed * Time.deltaTime);
        }
        else if(distance < NavDistance && time > NavTime)
        {
            MoveOk = false;
            PlayerDir = 0;

            if (cnt == 0)
            {
                Dash = true;

                if (player.transform.position.x < transform.position.x)
                    PlayerDir = -1;
                else
                    PlayerDir = 1;
                cnt++;
            }
        }
        else if (MoveOk)
        {
            if (dir == -1) GetComponent<SpriteRenderer>().flipX = false;
            else if (dir == 1) GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(new Vector3(dir, 0, 0) * MoveSpeed * Time.deltaTime);
        }
    }

    public void getDamaged(int d)
    {
        hp -= d;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if (Dash)
            {
                Dash = false;
                MoveOk = true;
                time = 0.0f;
                cnt = 0;
            }
            dir *= -1;
        }
        else if(collision.gameObject.tag == "Player")
        {
            if (Dash)
            {
                Dash = false;
                GameManager.Instance.WaitSeconds(1.0f);
                MoveOk = true;
                time = 0.0f;
            }
            else
            {
                interact.Attacked(1);
                dir *= -1;
            }
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
}
