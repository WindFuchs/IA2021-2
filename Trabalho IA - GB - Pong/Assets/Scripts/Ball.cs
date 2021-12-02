using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector2 startPosition;
    public bool hasColidedWithPaddle;

    private void Start()
    {
        Lunch();
        hasColidedWithPaddle = false;
        startPosition = transform.position;
    }

    private void Lunch()
    {
        float x = Random.Range(-1f, 1f) == 0 ? -1 : 1;
        float y = Random.Range(-1f, 1f);
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<Paddle>())
        {
            hasColidedWithPaddle = true;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + Random.Range(-3f, 3f));
        }
    }

    public bool getGasColidedWithPaddle()
    {
        return hasColidedWithPaddle;
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        Lunch();
    }
}
