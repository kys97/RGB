using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject buttons;
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            buttons.SetActive(true);
        }
    }

    public void ToStage1()
    {
        SceneManager.LoadScene(1);
    }
}
