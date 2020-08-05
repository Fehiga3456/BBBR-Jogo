using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanwAlvos : MonoBehaviour
{
    int randomAlvos;
    int randomPassado = 10;

    bool rodou =  true;
    public GameObject[] alvos;
    
    private void Start()
    {
        SpawnAlvos();

    }

   
    public void SpawnAlvos()
    {
        if (gameObject != null)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                randomAlvos = Random.Range(0, 2);
                while (randomAlvos == randomPassado && rodou)
                {
                    randomAlvos = Random.Range(0, 2);
                   // Debug.Log("rodou");
                }
                             
                randomPassado = randomAlvos;
                if (i == 1)
                {
                    rodou = false;
                    
                }
                //    Debug.Log(randomAlvos);
                Instantiate(alvos[randomAlvos], transform.GetChild(i).transform);
              
            }
        }      
    }
}
