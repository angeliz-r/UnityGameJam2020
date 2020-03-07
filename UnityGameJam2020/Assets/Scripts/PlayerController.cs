using System.Collections;
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
    private Animator _anim;


    private void Start()
    {
        stunValue = 0;
        _ds = GetComponent<DualShock4Input>();
        _myPos = transform.position;
        _rScore = FindObjectOfType<RoundScoring>();
        _bPlanter = GetComponent<BombPlanter>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, _dir, Color.red);
        hit = Physics2D.Raycast(transform.position, _faceDir, 1.0f, LayerMask.GetMask("Plantable"));
        if (stunValue > 0)
        {
            StunDuration();
            _anim.SetTrigger("Stun");
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.Space) || _ds.GetButtonDown(ControlCode.X)) && transform.position == _myPos)
                Plant();
            if (Input.GetKeyDown(KeyCode.F) || _ds.GetButtonDown(ControlCode.Square))
                Push();
        }
        
    }

    private void FixedUpdate()
    {
        if(stunValue <= 0)
            Move();
        if (bomb != null)
        {
            MoveBomb();
        }
    }

    void StunDuration()
    {
        stunValue -= Time.deltaTime;
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

        if ((Input.GetKey(KeyCode.W) || _ds.GetAxisRaw(ControlCode.LeftStickY) > 0) && transform.position == _myPos) {
            _faceDir = new Vector2(0, 1);
            if (_up && _myPos.y < max.position.y) {
                _myPos = new Vector2(_myPos.x, _myPos.y + 1);
            }
            
        } else if ((Input.GetKey(KeyCode.S) || _ds.GetAxisRaw(ControlCode.LeftStickY) < 0) && transform.position == _myPos) {
            _faceDir = new Vector2(0, -1);

            if (_down && _myPos.y > min.position.y) {
                _myPos = new Vector2(_myPos.x, _myPos.y - 1);
            }
            
        } else if ((Input.GetKey(KeyCode.A) || _ds.GetAxisRaw(ControlCode.LeftStickX) < 0) && transform.position == _myPos) {
            _faceDir = new Vector2(-1, 0);
            if (_left && _myPos.x > min.position.x) {
                _myPos = new Vector2(_myPos.x - 1, _myPos.y);
            }
            
            GetComponent<SpriteRenderer>().flipX = true;
        } else if ((Input.GetKey(KeyCode.D) || _ds.GetAxisRaw(ControlCode.LeftStickX) > 0) && transform.position == _myPos) {
            _faceDir = new Vector2(1, 0);
            if (_right && _myPos.x < max.position.x) {
                _myPos = new Vector2(_myPos.x + 1, _myPos.y);
            }
            
            GetComponent<SpriteRenderer>().flipX = false;
        }


        if (transform.position != _myPos)
        {
            transform.position -= Vector3.Normalize(transform.position - _myPos) / 33f;
            if (Vector3.Distance(transform.position, _myPos) <= 0.1f)
                transform.position = _myPos;
            AnimSetter();
        } else {
            _anim.SetBool("FaceSide", false);
            _anim.SetBool("FaceDown", false);
            _anim.SetBool("FaceUp", false);
        }
        _dir = transform.position + new Vector3(_faceDir.x, _faceDir.y, 0);


        void AnimSetter() {
            _anim.SetBool("FaceSide", (Input.GetKey(KeyCode.D) || _ds.GetAxisRaw(ControlCode.LeftStickX) > 0) 
                || (Input.GetKey(KeyCode.A) || _ds.GetAxisRaw(ControlCode.LeftStickX) < 0));

            _anim.SetBool("FaceDown", (Input.GetKey(KeyCode.S) || _ds.GetAxisRaw(ControlCode.LeftStickY) < 0));
            _anim.SetBool("FaceUp", (Input.GetKey(KeyCode.W) || _ds.GetAxisRaw(ControlCode.LeftStickY) > 0));
        }
    }
}
