using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 _myPos;
    public CheckCell[] checker;
    private bool _left, _right, _up, _down;
    private bool _pLeft, _pRight, _pUp, _pDown;
    public Transform min, max;

    private void Start()
    {
        _myPos = transform.position;
    }

    private void Update()
    {
        Move();
        if (Input.GetKey(KeyCode.Space))
            Plant();
    }

    private void Plant()
    {
        _pUp = checker[0].canPlant;
        _pRight = checker[1].canPlant;
        _pDown = checker[2].canPlant;
        _pLeft = checker[3].canPlant;
        
    }
    private void Move()
    {
        _up = checker[0].canMove;
        _right = checker[1].canMove;
        _down = checker[2].canMove;
        _left = checker[3].canMove;


        if (Input.GetKey(KeyCode.W) && _up)
        {
            if (Vector3.Distance(transform.position, _myPos) <= 0.05f && _myPos.y < max.position.y)
            {
                _myPos = new Vector2(_myPos.x, _myPos.y + 1);
            }
        }
        if (Input.GetKey(KeyCode.S) && _down)
        {
            if (Vector3.Distance(transform.position, _myPos) <= 0.05f && _myPos.y > min.position.y)
            {
                _myPos = new Vector2(_myPos.x, _myPos.y - 1);
            }
        }
        if (Input.GetKey(KeyCode.A) && _left)
        {
            if (Vector3.Distance(transform.position, _myPos) <= 0.05f && _myPos.x > min.position.x)
            {
                _myPos = new Vector2(_myPos.x - 1, _myPos.y);
            }
        }
        if (Input.GetKey(KeyCode.D) && _right)
        {
            if (Vector3.Distance(transform.position, _myPos) <= 0.05f && _myPos.x < max.position.x)
            {
                _myPos = new Vector2(_myPos.x + 1, _myPos.y);
            }
        }
        if (transform.position != _myPos)
        {
            transform.position -= Vector3.Normalize(transform.position - _myPos) / 10f;
        }
    }
}
