using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1OverTrigger : MonoBehaviour
{
    public bool clearStage1 = false;
    public GameObject fadeOutPanel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            clearStage1 = true;
            StartCoroutine(Fade());
        }
    }

    IEnumerator Fade()
    {
        fadeOutPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
        yield return new WaitForSeconds(3f);
        fadeOutPanel.SetActive(false);
    }
}
