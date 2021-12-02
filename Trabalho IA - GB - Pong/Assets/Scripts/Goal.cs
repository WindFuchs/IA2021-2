using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Goal;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>())
        {
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
