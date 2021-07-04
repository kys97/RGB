using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;
    public int maxGage = 11;
    public int maxHealth = 5;
    public int damage = 1;
    public int s_damage = 4;
    public float runSpeed = 40f;
    private void Awake()
    {
        instance = this;
        slider.value = gage;
        health = maxHealth;
    }
    public Slider slider;

    private int gage = 5;
    public void ChangeGage(int gageChangeAmount)
    {
        if (gage + gageChangeAmount < 0)
            gage = 0;
        if (gage + gageChangeAmount > maxGage)
            gage = maxGage;
        gage += gageChangeAmount;
        slider.value = gage;
    }
    public int GetGage()
    {
        return gage;
    }

    private int health;
    public void ChangeHealth(int healthChangeAmount)
    {
        health += healthChangeAmount;
        if (health <= 0)
            GameManager.Instance.GameOver();
    }
    public void IncreaseDamage(int amount, float time)
    {
        StartCoroutine(DamageBoost(amount,time));
    }
    public void IncreaseSpeed(int amount, float time)
    {
        StartCoroutine(SpeedBoost(amount, time));
    }
    IEnumerator DamageBoost(int amount, float time)
    {
        damage += amount;
        yield return new WaitForSeconds(time);
        damage -= amount;
    }
    IEnumerator SpeedBoost(int amount, float time)
    {
        runSpeed += amount;
        yield return new WaitForSeconds(time);
        runSpeed -= amount;
    }
}
