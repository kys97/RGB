using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteo : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int ExplosionDamageRange;
    public int meteoDamage;

    PlayerInteract interact;
    private GameObject player;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        interact = player.GetComponent<PlayerInteract>();
        distance = 10000;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        if (collision.gameObject.tag != "Player")
        {
            if(distance < ExplosionDamageRange)
            {
                interact.Attacked(meteoDamage);
            }
        }

        if(collision.gameObject.tag == "Player")
        {
            interact.Attacked(meteoDamage);
        }
        Destroy(gameObject);

    }
} 
