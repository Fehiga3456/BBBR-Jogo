using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
   
   public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }



}
