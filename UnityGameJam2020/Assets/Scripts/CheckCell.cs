using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCell : MonoBehaviour
{
    public bool canMove;
    private void Start()
    {
        canMove = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("NotWalkable"))
        {
            canMove = false;
        }
        if (collision.CompareTag("ActualBomb"))
        {
            canMove = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ActualBomb") || collision.CompareTag("NotWalkable"))
        {
            canMove = true;
        }
    }
}
