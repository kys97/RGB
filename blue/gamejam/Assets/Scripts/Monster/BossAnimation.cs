using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    public float time =5f;
    Animator animator;
    BossBulletHolder shooter;
   
    int beamHash;
    int swordHash;
    
    private void Awake()
    {
        shooter = GetComponentInChildren<BossBulletHolder>();
        animator = GetComponent<Animator>();
        beamHash = Animator.StringToHash("Boss_beam");
        swordHash = Animator.StringToHash("Boss_sword");
    }
    private void Start()
    {
        StartCoroutine(BossPatern());   
    }

    
    IEnumerator BossPatern()
    {
        while (true)
        {
            shooter.fireMode = BossBulletHolder.FireMode.none;
            yield return new WaitForSeconds(time);
            animator.SetTrigger(swordHash);         
            yield return new WaitForSeconds(time);
            shooter.fireMode = BossBulletHolder.FireMode.swing;
            yield return new WaitForSeconds(time);
            shooter.fireMode = BossBulletHolder.FireMode.none;
            yield return new WaitForSeconds(time);
            animator.SetTrigger(beamHash);
            yield return new WaitForSeconds(time);
            shooter.fireMode = BossBulletHolder.FireMode.none;
            yield return new WaitForSeconds(time);
            shooter.fireMode = BossBulletHolder.FireMode.circle;
            yield return new WaitForSeconds(time);

        }
    }
}
