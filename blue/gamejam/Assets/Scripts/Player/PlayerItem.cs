using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    PlayerInteract interact;
    public Items curItem;
    public float barrierTime;
    public int gageRecoverAmount;
    public int damageIncreaseAmount;
    public float damageIncreaseTime;
    public int speedIncreaseAmount;
    public float speedIncreaseTime;
    private void Awake()
    {
        interact = GetComponent<PlayerInteract>();
    }
    private void Start()
    {
        curItem = Items.none;
    }
    private void Update()
    {
        if(Input.GetButtonDown("Use Item"))
        {
            switch (curItem)
            {
                case Items.none:
                    return;
                case Items.barrier:
                    interact.ActiveShield(barrierTime);
                    break;
                case Items.gage:
                    PlayerStat.instance.ChangeGage(gageRecoverAmount);
                    break;
                case Items.damage:
                    PlayerStat.instance.IncreaseDamage(damageIncreaseAmount, damageIncreaseTime);
                    break;
                case Items.speed:
                    PlayerStat.instance.IncreaseSpeed(speedIncreaseAmount, speedIncreaseTime);
                    break;

            }
        }
    }

    public enum Items
    {
        barrier,
        gage,
        speed,
        damage,
        none
    }
}
