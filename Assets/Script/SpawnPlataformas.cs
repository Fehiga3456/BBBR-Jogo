using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlataformas : MonoBehaviour
{


    public GameObject[] plataformas = new GameObject[2];//Plataforma do jogo
    public GameObject[] plataformasPos = new GameObject[4];//Locais das plataformas


    int randomPlataformas;
    int randomPassado = 10;

    // Start is called before the first frame update
    void Start()
    {
        Spanw();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spanw()
    {
        //PLATAFORMAS
        for (int i = 0; i < plataformas.Length; ++i)
        {
            randomPlataformas = Random.Range(0,3);
            
            while (randomPlataformas == randomPassado )
            {
                randomPlataformas = Random.Range(0, 3);

            }
            randomPassado = randomPlataformas;          
            Instantiate(plataformas[i], plataformasPos[randomPlataformas].transform);

            
           // Debug.Log(randomPassado);
        }
    }

  

}
