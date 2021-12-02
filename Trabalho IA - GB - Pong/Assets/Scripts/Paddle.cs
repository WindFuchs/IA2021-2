using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Paddle : Agent
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector2 startPosition;

    private float movement;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform oponent;

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Debug.LogFormat("Player " + (isPlayer1 ? 1 : 2) + " - action: " + actions.ContinuousActions[0]);
        float moveY = actions.DiscreteActions[0];

        int direction = 0;

        if (moveY == 0)
        {
            direction = 0;
        }
        else if (moveY == 1)
        {
            direction = -1;
        }
        else if (moveY == 2)
        {
            direction = 1;
        }

        rb.velocity = new Vector2(rb.velocity.x, direction * Time.deltaTime * speed);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.y); //Self Position;
        sensor.AddObservation(ball.position.y); //Ball Position;
        sensor.AddObservation(ball.position.x); //Ball Position;
        // sensor.AddObservation(oponent.position); //Designated Oponent Position;
        // Debug.Log(
        //     "Self Position - " + transform.position.y + "\n" +
        //     "Ball Position - " + ball.position.x + " | " + ball.position.y + "\n" +
        //     "Enemy Position - " + oponent.position.y + "\n"
        //     );
    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void movePaddle(float moveControl)
    {
        rb.velocity = new Vector2(rb.velocity.x, moveControl * speed);
    }

    public void rewardBot(bool hasColidedWithPaddle = false)
    {
        if (hasColidedWithPaddle)
        {
            AddReward(0.5f);
        }
        EndEpisode();
    }

    public void penalizeBot(bool hasColidedWithPaddle = false)
    {
        AddReward(-0.25f);
        EndEpisode();
    }

    private void Update()
    {
        // if (isPlayer1)
        // {
        //     movement = Input.GetAxis("Vertical2");
        // }
        // else
        // {
        //     movement = Input.GetAxis("Vertical");
        // }
        // rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }

    // public override void OnEpisodeBegin()
    // {
    //     Reset();
    // }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //TODO
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent<Ball>(out Ball ball))
        {
            AddReward(1.0f);
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
