using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlat : MonoBehaviour
{
    int dificuldade;

 
    public float speed;

    bool MoveRight = true;

    private void Start()
    {
        dificuldade = PlayerPrefs.GetInt("dificuldade");
        
       
    }

    private void Update()
    {
        if (dificuldade == 1)
        {
            Move();
        }
        else
        {

        }

    }

    void Move()
    {
        if (transform.localPosition.x > 1f)
        {
            MoveRight = false;
        }
        if (transform.localPosition.x < -1f)
        {
            MoveRight = true;
        }

        if (MoveRight)
        {
            transform.localPosition = new Vector2(transform.localPosition.x + speed * Time.deltaTime, transform.localPosition.y);
        }
        else
            transform.localPosition = new Vector2(transform.localPosition.x - speed * Time.deltaTime, transform.localPosition.y);
    }



}
