using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    public GameObject buttons;
    public GameObject image;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            image.SetActive(true);
            buttons.SetActive(true);
        }
    }

    public void ToStage1()
    {
        SceneManager.LoadScene(1);
    }

    public void ToStage2()
    {
        SceneManager.LoadScene(2);
    }
}
