﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public Vector3 _myPos;
    private DualShock4Input _ds;
    public CheckCell[] checker;
    public GameObject plant;
    public float stunValue;
    private bool _left, _right, _up, _down;
    public Transform min, max;
    public GameObject bomb;
    private Vector2 _faceDir, _dir;
    private Vector3 _bombDir, _bombDes;
    RaycastHit2D hit;
    public event Action EventPlant = () => { };
    private RoundScoring _rScore;
    private BombPlanter _bPlanter;
    private AudioController _audio;

    private Animator _anim;

    private bool _canPlayStunned;
    private void Start()
    {
        stunValue = 0;
        _ds = GetComponent<DualShock4Input>();
        _myPos = transform.position;
        _rScore = FindObjectOfType<RoundScoring>();
        _bPlanter = GetComponent<BombPlanter>();
        _audio = GetComponent<AudioController>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, _dir, Color.red);
        hit = Physics2D.Raycast(transform.position, _faceDir, 1.0f, LayerMask.GetMask("Plantable"));
        if (stunValue > 0)
        {
            StunDuration();
            PlayStunSFX();
        }
        else
        {
            if (/*(Input.GetKeyDown(KeyCode.Space) ||*/ _ds.GetButtonDown(ControlCode.X) && transform.position == _myPos)
                Plant();
            if (/*Input.GetKeyDown(KeyCode.F) ||*/ _ds.GetButtonDown(ControlCode.Square))
                Push();

            //keyboard controls
            if (_ds.GetJoystickNumber() == 1) //player 1
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Plant();
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Push();
                }
            }
            else //player 2
            {
                if (Input.GetKeyDown(KeyCode.Comma))
                {
                    Plant();
                }
                if (Input.GetKeyDown(KeyCode.Alpha8))
                {
                    Push();
                }
            }
        }
        
    }
    public void ResetPos()
    {
        _myPos = transform.position;
    }
    private void FixedUpdate()
    {
        if(stunValue <= 0)
        {
            Move();
            SetAnimWalk();
        }

        if (bomb != null)
        {
            MoveBomb();
        }
    }

    void PlayStunSFX()
    {
        if (_canPlayStunned && stunValue > 0)
        {
            _audio.PlaySoundEffect(SFXCollection.stun);
            _canPlayStunned = false;
        }
    }
    void StunDuration()
    {
        stunValue -= Time.deltaTime;
        if (stunValue <= 0) {
            _canPlayStunned = true;
        }
    }

    public void Plant()
    {
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Land")
                    && transform.position == _myPos)
            {
                GameObject obj = Instantiate(plant, _dir, Quaternion.identity);
                obj.transform.parent = hit.transform;
                if (obj.GetComponent<Plants>().myPlant == PlayerType.MAN)
                {
                    _audio.PlaySoundEffect(SFXCollection.makebldg_sfx);
                }
                else
                {
                    _audio.PlaySoundEffect(SFXCollection.makeplant_sfx);
                }
                ScoreUpdateOnPlant();
                EventPlant();
            }
        }
    }

    PlayerType MyType() {
        return _bPlanter.playerType;
    }

    void ScoreUpdateOnPlant() {
        if (MyType() == PlayerType.MAN)
            _rScore.manScore.AddLiveScore();
        else if (MyType() == PlayerType.NATURE)
            _rScore.natureScore.AddLiveScore();
    }

    void Push()
    {
        if (hit.collider != null)
        {
            if(hit.collider.CompareTag("ActualBomb") && transform.position == _myPos)
            {
                bomb = hit.collider.gameObject;
                _bombDes = transform.position - bomb.transform.position;
                _bombDes *= 2f;
                _bombDes = bomb.transform.position - _bombDes;
                if (_bombDes.x < min.position.x)
                    _bombDes = new Vector3(min.position.x, _bombDes.y, _bombDes.z);
                if (_bombDes.x > max.position.x)
                    _bombDes = new Vector3(max.position.x, _bombDes.y, _bombDes.z);
                if (_bombDes.y < min.position.y)
                    _bombDes = new Vector3(_bombDes.x, min.position.y, _bombDes.z);
                if (_bombDes.y > max.position.y)
                    _bombDes = new Vector3(_bombDes.x, max.position.y, _bombDes.z);
            }
        }
    }
    void MoveBomb()
    {
        if (bomb.transform.position != _bombDes)
            bomb.transform.position = Vector3.MoveTowards(bomb.transform.position, _bombDes, 0.1f);
    }
    private void Move()
    {
        _up = checker[0].canMove;
        _right = checker[1].canMove;
        _down = checker[2].canMove;
        _left = checker[3].canMove;

        if ((_ds.GetAxisRaw(ControlCode.LeftStickY) > 0) && transform.position == _myPos)
        {
            _faceDir = new Vector2(0, 1);
            if (_up && _myPos.y < max.position.y)
            {
                _myPos = new Vector2(_myPos.x, _myPos.y + 1);
            }
        }
        else if (( _ds.GetAxisRaw(ControlCode.LeftStickY) < 0) && transform.position == _myPos)
        {
            _faceDir = new Vector2(0, -1);

            if (_down && _myPos.y > min.position.y)
            {
                _myPos = new Vector2(_myPos.x, _myPos.y - 1);
            }
        }
        else if (( _ds.GetAxisRaw(ControlCode.LeftStickX) < 0) && transform.position == _myPos)
        {
            _faceDir = new Vector2(-1, 0);
            if (_left && _myPos.x > min.position.x)
            {             
                _myPos = new Vector2(_myPos.x - 1, _myPos.y);
            }
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (( _ds.GetAxisRaw(ControlCode.LeftStickX) > 0) && transform.position == _myPos)
        {
            _faceDir = new Vector2(1, 0);
            if (_right && _myPos.x < max.position.x)
            {
                _myPos = new Vector2(_myPos.x + 1, _myPos.y);
            }
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //keyboard controls
        if (_ds.GetJoystickNumber() == 1) //player 1
        {
            if (Input.GetKey(KeyCode.W) && transform.position == _myPos) 
            {
                _faceDir = new Vector2(0, 1);
                if (_up && _myPos.y < max.position.y)
                {
                    _myPos = new Vector2(_myPos.x, _myPos.y + 1);
                }
            }
            else if (Input.GetKey(KeyCode.S) && transform.position == _myPos)
            {
                _faceDir = new Vector2(0, -1);

                if (_down && _myPos.y > min.position.y)
                {
                    _myPos = new Vector2(_myPos.x, _myPos.y - 1);
                }
            }
            else if (Input.GetKey(KeyCode.A) && transform.position == _myPos)
            {
                _faceDir = new Vector2(-1, 0);
                if (_left && _myPos.x > min.position.x)
                {
                    _myPos = new Vector2(_myPos.x - 1, _myPos.y);
                }
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey(KeyCode.D) && transform.position == _myPos)
            {
                _faceDir = new Vector2(1, 0);
                if (_right && _myPos.x < max.position.x)
                {
                    _myPos = new Vector2(_myPos.x + 1, _myPos.y);
                }
                GetComponent<SpriteRenderer>().flipX = false;
            }

        }
        else //player 2
        {
            if (Input.GetKey(KeyCode.I) && transform.position == _myPos)
            {
                _faceDir = new Vector2(0, 1);
                if (_up && _myPos.y < max.position.y)
                {
                    _myPos = new Vector2(_myPos.x, _myPos.y + 1);
                }
            }
            else if (Input.GetKey(KeyCode.K) && transform.position == _myPos)
            {
                _faceDir = new Vector2(0, -1);

                if (_down && _myPos.y > min.position.y)
                {
                    _myPos = new Vector2(_myPos.x, _myPos.y - 1);
                }
            }
            else if (Input.GetKey(KeyCode.J) && transform.position == _myPos)
            {
                _faceDir = new Vector2(-1, 0);
                if (_left && _myPos.x > min.position.x)
                {
                    _myPos = new Vector2(_myPos.x - 1, _myPos.y);
                }
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey(KeyCode.L) && transform.position == _myPos)
            {
                _faceDir = new Vector2(1, 0);
                if (_right && _myPos.x < max.position.x)
                {
                    _myPos = new Vector2(_myPos.x + 1, _myPos.y);
                }
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (transform.position != _myPos) {
            transform.position -= Vector3.Normalize(transform.position - _myPos) / 33f;
            if (Vector3.Distance(transform.position, _myPos) <= 0.1f)
                transform.position = _myPos;
            
        } else {
            SetAnimStop();
        }
        _dir = transform.position + new Vector3(_faceDir.x, _faceDir.y, 0);
    }

    void SetAnimStop() {
        _anim.SetBool("FaceUp", false);
        _anim.SetBool("FaceDown", false);
        _anim.SetBool("FaceSide", false);
    }

    void SetAnimWalk() {
        _anim.SetBool("FaceUp", (Input.GetKey(KeyCode.W) || _ds.GetAxisRaw(ControlCode.LeftStickY) > 0));
        _anim.SetBool("FaceDown", (Input.GetKey(KeyCode.S) || _ds.GetAxisRaw(ControlCode.LeftStickY) < 0));
        _anim.SetBool("FaceSide", (Input.GetKey(KeyCode.A) || _ds.GetAxisRaw(ControlCode.LeftStickX) < 0)
            || (Input.GetKey(KeyCode.D) || _ds.GetAxisRaw(ControlCode.LeftStickX) > 0));
    }
}
