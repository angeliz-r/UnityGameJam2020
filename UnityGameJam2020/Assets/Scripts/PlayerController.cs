﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Vector3 _myPos;
    private DualShock4Input _ds;
    public CheckCell[] checker;
    public GameObject plant;
    private bool _left, _right, _up, _down;
    public Transform min, max;
    private Vector2 _faceDir, _dir;
    RaycastHit2D hit;
    public event Action EventPlant = () => { };
    private void Start()
    {
        _ds = GetComponent<DualShock4Input>();
        _myPos = transform.position;
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, _dir, Color.red);
        hit = Physics2D.Raycast(transform.position, _faceDir, 1.0f, LayerMask.GetMask("Plantable"));
        if ((Input.GetKeyDown(KeyCode.Space) || _ds.GetButtonDown(ControlCode.X))&& transform.position == _myPos)
            Plant();
    }

    private void FixedUpdate()
    {
        Move();
    }
    public void Plant()
    {
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Land")
                    && transform.position == _myPos)
            {
                Debug.Log("PLANT");
                GameObject obj = Instantiate(plant, _dir, Quaternion.identity);
                obj.transform.parent = hit.transform;
                EventPlant();
            }
        }
    }

    private void Move()
    {
        _up = checker[0].canMove;
        _right = checker[1].canMove;
        _down = checker[2].canMove;
        _left = checker[3].canMove;

        if ((Input.GetKey(KeyCode.W) || _ds.GetAxisRaw(ControlCode.LeftStickY) > 0) && transform.position == _myPos)
        {
            _faceDir = new Vector2(0, 1);
            if (_up && _myPos.y < max.position.y)
            {
                _myPos = new Vector2(_myPos.x, _myPos.y + 1);
            }
        }
        else if ((Input.GetKey(KeyCode.S) || _ds.GetAxisRaw(ControlCode.LeftStickY) < 0) && transform.position == _myPos)
        {
            _faceDir = new Vector2(0, -1);

            if (_down && _myPos.y > min.position.y)
            {
                _myPos = new Vector2(_myPos.x, _myPos.y - 1);
            }
        }
        else if ((Input.GetKey(KeyCode.A) || _ds.GetAxisRaw(ControlCode.LeftStickX) < 0) && transform.position == _myPos)
        {
            _faceDir = new Vector2(-1, 0);
            if (_left && _myPos.x > min.position.x)
            {             
                _myPos = new Vector2(_myPos.x - 1, _myPos.y);
            }
        }
        else if ((Input.GetKey(KeyCode.D) || _ds.GetAxisRaw(ControlCode.LeftStickX) > 0) && transform.position == _myPos)
        {
            _faceDir = new Vector2(1, 0);
            if (_right && _myPos.x < max.position.x)
            {
                _myPos = new Vector2(_myPos.x + 1, _myPos.y);
            }
        }
        if (transform.position != _myPos)
        {
            transform.position -= Vector3.Normalize(transform.position - _myPos) / 33f;
            if (Vector3.Distance(transform.position, _myPos) <= 0.1f)
                transform.position = _myPos;
        }
        _dir = transform.position + new Vector3(_faceDir.x, _faceDir.y, 0);
    }
}
