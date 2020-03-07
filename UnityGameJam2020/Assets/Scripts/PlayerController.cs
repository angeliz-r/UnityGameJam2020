using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Vector3 _myPos;
    public CheckCell[] checker;
    private bool _left, _right, _up, _down;
    public Transform min, max;
    public event Action EventPlant = () => { };
    private void Start()
    {
        _myPos = transform.position;
    }

    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && transform.position == _myPos)
            Plant();
    }

    public void Plant()
    {
        if(transform.position == _myPos)
        EventPlant();
    }

    private void Move()
    {
        _up = checker[0].canMove;
        _right = checker[1].canMove;
        _down = checker[2].canMove;
        _left = checker[3].canMove;


        if (Input.GetKey(KeyCode.W) && _up)
        {
            if (transform.position == _myPos && _myPos.y < max.position.y)
            {
                _myPos = new Vector2(_myPos.x, _myPos.y + 1);
            }
        }
        else if (Input.GetKey(KeyCode.S) && _down)
        {
            if (transform.position == _myPos && _myPos.y > min.position.y)
            {
                _myPos = new Vector2(_myPos.x, _myPos.y - 1);
            }
        }
        else if (Input.GetKey(KeyCode.A) && _left)
        {
            if (transform.position == _myPos && _myPos.x > min.position.x)
            {
                _myPos = new Vector2(_myPos.x - 1, _myPos.y);
            }
        }
        else if (Input.GetKey(KeyCode.D) && _right)
        {
            if (transform.position == _myPos && _myPos.x < max.position.x)
            {
                _myPos = new Vector2(_myPos.x + 1, _myPos.y);
            }
        }
        if (transform.position != _myPos)
        {
            transform.position -= Vector3.Normalize(transform.position - _myPos) / 50f;
            if (Vector3.Distance(transform.position, _myPos) <= 0.1f)
                transform.position = _myPos;
        }
    }
}
