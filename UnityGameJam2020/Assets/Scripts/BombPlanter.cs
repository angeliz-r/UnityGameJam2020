using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType {
    NATURE, MAN
}

//Part of Player Component
public class BombPlanter : MonoBehaviour
{
    [SerializeField] PlayerType _playerType;
    [SerializeField] PlayerController player;
    [SerializeField]
    private GameObject _roundBomb;
    [SerializeField]
    private GameObject _crossBomb;

    private bool _hasBomb = false;
    private DualShock4Input ds;

    public PlayerType playerType { get { return _playerType; } }


    private void OnEnable() {
        _hasBomb = true;
    }

    private void Start() {
        player = GetComponent<PlayerController>();
        ds = GetComponent<DualShock4Input>();
    }

    private void Update()
    {
        PlantABomb();
    }
    public void SetHasBomb(bool b) {
        _hasBomb = b;
    }

    public void PlantABomb() {
        if (_hasBomb && transform.position == player._myPos) {
            if (ds.GetButtonDown(ControlCode.Circle)) {
                // Plant Round Bomb
                 SetBomb(_playerType, _roundBomb);
            }
            if (ds.GetButtonDown(ControlCode.Triangle)) {
                // Plant Cross Bomb
                 SetBomb(_playerType, _crossBomb);
            }
        }
    }

    private void SetBomb(PlayerType type, GameObject bomb) {
        var b = Instantiate(bomb, transform.position, Quaternion.identity);
        b.GetComponent<Bomb>().SetType(type);
        _hasBomb = false;
    }
}


