using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Goal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Detected Colision");
        if (collision.GetComponent<Ball>())
        {
            Debug.Log("Detected Colision");
            if (isPlayer1Goal)
            {
                GameObject.Find(transform.parent.name).GetComponentInChildren<GameManager>().Player2Scored();
            }
            else
            {
                GameObject.Find(transform.parent.name).GetComponentInChildren<GameManager>().Player1Scored();
            }
        }
    }
}
