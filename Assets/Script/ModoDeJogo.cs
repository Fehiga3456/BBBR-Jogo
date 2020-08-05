using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ModoDeJogo : MonoBehaviour
{

    public Toggle facil;
    public Toggle dificil;

  
    private void Start()
    {
       
        if (PlayerPrefs.GetInt("dificuldade") == 0)
        {
            facil.isOn = true;
        }
        else if (PlayerPrefs.GetInt("dificuldade") == 1)
        {
            dificil.isOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("dificuldade", 0);
        }

    }


    public void ActiveToggle()
    {
        if (facil.isOn)
        {
           
            PlayerPrefs.SetInt("dificuldade", 0);
            Debug.Log("false");
        }
        else if (dificil.isOn)
        {
            PlayerPrefs.SetInt("dificuldade", 1);
            Debug.Log("true");
        }
    }

  

}
