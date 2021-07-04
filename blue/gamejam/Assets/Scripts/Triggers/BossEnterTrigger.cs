using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnterTrigger : MonoBehaviour
{
    public Transform bossCam;
    public Transform playerSpawn;
    public GameObject cmCam;
    public GameObject fadeOutPanel;
    public GameObject boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EnterBoss();    
        }
        boss.SetActive(true);
    }
    void EnterBoss()
    {
        cmCam.SetActive(false);
         
        StartCoroutine(Fade());
    }
    IEnumerator Fade()
    {
        fadeOutPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = bossCam.position.x;
        Camera.main.transform.position = camPos;
        fadeOutPanel.SetActive(false);
    }
}
