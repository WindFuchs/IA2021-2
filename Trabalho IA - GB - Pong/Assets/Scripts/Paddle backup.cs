using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleB : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector2 startPosition;

    private float movement;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (isPlayer1)
        {
            movement = Input.GetAxis("Vertical2");
        }
        else
        {
            movement = Input.GetAxis("Vertical");
        }
        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
