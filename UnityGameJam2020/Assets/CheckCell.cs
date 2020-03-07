using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCell : MonoBehaviour
{
    public GameObject plant;
    public bool canMove;
    public bool canPlant;
    private bool _plantOnce;


    private void Start()
    {
        PlayerController playerPC = FindObjectOfType<PlayerController>(); 
        canMove = true;
        canPlant = false;
        _plantOnce = false;
        playerPC.EventPlant += OnPlant;
    }

    void OnPlant()
    {
        if(canPlant && !_plantOnce)
        {
            Instantiate(plant, transform.position, Quaternion.identity);
            _plantOnce = true;
            StartCoroutine(Buffer());
        }
    }

    IEnumerator Buffer()
    {
        yield return new WaitForSeconds(0.1f);
        _plantOnce = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NotWalkable") || collision.CompareTag("Player"))
        {
            canMove = false;
        }
        if (!collision.CompareTag("Plant"))
        {
            if (collision.CompareTag("Land"))
            {
                canPlant = true;
            }
        }
        if (collision.CompareTag("Plant"))
            canPlant = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NotWalkable") || collision.CompareTag("Player"))
        {
            canMove = true;
        }
        if (collision.CompareTag("Land"))
        {
            canPlant = false;
        }
    }
}
