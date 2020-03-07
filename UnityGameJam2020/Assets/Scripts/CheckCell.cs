using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCell : MonoBehaviour
{
    private PlayerController playerPC;
    public bool canMove;
    private void Start()
    {
        playerPC = FindObjectOfType<PlayerController>();
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NotWalkable"))
        {
            canMove = false;
        }
        if (collision.CompareTag("ActualBomb"))
        {
            playerPC.bomb = collision.transform.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NotWalkable"))
        {
            canMove = true;
        }
    }
}
