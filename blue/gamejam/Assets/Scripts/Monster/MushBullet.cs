using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushBullet : MonoBehaviour
{
    [SerializeField] private int BulletSpeed;
    [SerializeField] private float AttackDamage;
    [HideInInspector] public int dir;
    [HideInInspector] public Vector3 FirePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(dir, 0, 0) * BulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
