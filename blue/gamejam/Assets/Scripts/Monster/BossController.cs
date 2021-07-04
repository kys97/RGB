using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public int maxHealth;
    [SerializeField]
    int health;

    private void Awake()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void TakeDamage(int damage)
    {
        health -= damage;
        if(health < 0)
        {
            StopAllCoroutines();
            gameObject.SetActive(false);
            Debug.Log("와");
            return;
        }
        StartCoroutine(flash());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Bullet")
        {
            TakeDamage(PlayerStat.instance.damage);
        }
        if(collision.tag == "s_Bullet")
        {
            TakeDamage(PlayerStat.instance.s_damage);
        }
    }

    IEnumerator flash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
