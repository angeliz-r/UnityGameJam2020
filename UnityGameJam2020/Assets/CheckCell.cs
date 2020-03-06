using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCell : MonoBehaviour
{
    public bool canMove;
    public bool canPlant;

    private void Start()
    {
        canMove = true;
        canPlant = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant") || collision.CompareTag("Player"))
        {
            canMove = false;
        }
        if(collision.CompareTag("Plant"))
        {
            canPlant = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plant") || collision.CompareTag("Player"))
        {
            canMove = true;
        }
        if (collision.CompareTag("Plant"))
        {
            canPlant = false;
        }
    }
}
