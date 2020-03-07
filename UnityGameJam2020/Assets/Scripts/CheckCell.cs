using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCell : MonoBehaviour
{
    public bool canMove;
    private void Start()
    {
        PlayerController playerPC = FindObjectOfType<PlayerController>(); 
        canMove = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NotWalkable") || collision.CompareTag("Player"))
        {
            canMove = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NotWalkable") || collision.CompareTag("Player"))
        {
            canMove = true;
        }
        else if (collision.CompareTag("NotWalkable") && collision.CompareTag("Player"))
        {
            canMove = true;
        }
    }
}
