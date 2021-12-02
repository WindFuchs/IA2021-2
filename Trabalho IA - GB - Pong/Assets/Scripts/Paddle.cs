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
        // Debug.LogFormat("Player " + (isPlayer1 ? 1 : 2) + " - action:" + actions.ContinuousActions[0]);
        float moveY = actions.ContinuousActions[0];

        movePaddle(moveY);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position); //Self Position;
        sensor.AddObservation(ball.position); //Ball Position;
        sensor.AddObservation(oponent.position); //Designated Oponent Position;

    }

    private void Start()
    {
        startPosition = transform.position;
    }

    private void movePaddle(float moveControl)
    {
        rb.velocity = new Vector2(rb.velocity.x, moveControl * Time.deltaTime * speed);
    }

    public void rewardBot()
    {
        SetReward(1.0f);
        EndEpisode();
    }

    public void penalizeBot()
    {
        SetReward(-0.3f);
        EndEpisode();
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
            AddReward(0.3f);
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
