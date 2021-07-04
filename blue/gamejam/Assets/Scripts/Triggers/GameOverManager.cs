using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RePlay()
    {
        SceneManager.LoadScene(1);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(0);
    }
}
