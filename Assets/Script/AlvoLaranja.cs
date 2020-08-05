using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlvoLaranja : MonoBehaviour
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
        if (collision.tag == "BallSaiuLaranja" && gameManager.triggou == false)
        {
            Destroy(gameObject);
            gameManager.numeroDestruidosAlvos++;
            gameManager.triggou = true;
            Debug.Log("Certo");

        }
        if (collision.tag == "BallSaiuAzul" && gameManager.triggou == false)
        {
            gameManager.currentErrosInt++;
            gameManager.triggou = true;
            Debug.Log("BAETEU ERERADOOO");
        }
    }
  
}
