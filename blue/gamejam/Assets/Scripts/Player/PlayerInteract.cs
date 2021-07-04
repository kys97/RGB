using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject shield;
    public float knockbackForce;
    public float bombRadius;

    CharacterController2D controller;
    Rigidbody2D playerRigidbody;
    Animator animator;
    PlayerItem playerItem;
    int attackedToHash;
    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        playerItem = GetComponent<PlayerItem>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //collider = GetComponent<BoxCollider2D>();
        attackedToHash = Animator.StringToHash("Attacked");
    }
    public void Attacked(int damage)
    {
        PlayerStat.instance.ChangeHealth(-damage);
        controller.SpecialAttack(bombRadius);
        playerRigidbody.AddForce(new Vector2(-1.2f, 1) * knockbackForce);
        animator.Play(attackedToHash);
        
    }
    public void ActiveShield(float time)
    {
        StartCoroutine(StartShield(time));

    }
    IEnumerator StartShield(float time)
    {
        shield.SetActive(true);
        yield return new WaitForSeconds(time);
        shield.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Death")
        {
            GameManager.Instance.GameOver();
        }
        if(collision.tag == "Bullet_Blue")
        {
            PlayerStat.instance.ChangeGage(1);
        }
        if(collision.tag == "Bullet_Enemy")
        {
            Attacked(1);
        }
        if (collision.tag == "Barrier")
        {
            playerItem.curItem = PlayerItem.Items.barrier;
        }
        else if (collision.tag == "Speed")
            playerItem.curItem = PlayerItem.Items.speed;
        else if (collision.tag == "Damage")
            playerItem.curItem = PlayerItem.Items.damage;
        else if (collision.tag == "Gage")
            playerItem.curItem = PlayerItem.Items.gage;
        
    }
}
