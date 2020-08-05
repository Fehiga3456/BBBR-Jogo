using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D col;
    ParticleSystem particleSystem;
    SpriteRenderer spriteRenderer;
    GameObject gameManagerObj;
    CircleCollider2D circleCollider;


    GameManager gameManager;


    public Vector3 pos { get { return transform.position; } }


    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        gameManagerObj = GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        circleCollider = GetComponent<CircleCollider2D>();
    }


    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);

    }
    public void ActivateRb()
    {
        rb.isKinematic = false;

    }
    public void DesactivateRb()
    {
        rb.isKinematic = true;
        rb.angularVelocity = 0f;
        rb.velocity = Vector3.zero;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        gameManager.foiDestuido = true;
        DesactivateRb();
        StartCoroutine(AtivaParticula());

    }
    public IEnumerator AtivaParticula()
    {
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
        particleSystem.Play();
        yield return new WaitForSeconds(particleSystem.main.startLifetime.constantMax);
        Destroy(gameObject);

    }
}
