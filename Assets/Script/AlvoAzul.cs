using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlvoAzul : MonoBehaviour
{

    GameObject gameManagerObj;

    GameManager gameManager;
  

    private void Start()
    {
      
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BallSaiuAzul" && gameManager.triggou == false)
        {
            Destroy(gameObject);
            gameManager.triggou = true;
            gameManager.numeroDestruidosAlvos++;
            Debug.Log("BAETEU CERTO");
        }
        if (collision.tag == "BallSaiuLaranja" && gameManager.triggou == false)
        {
            gameManager.currentErrosInt++;
            gameManager.triggou = true;
            Debug.Log("BAETEU Errado");
        }
    }

   

}
