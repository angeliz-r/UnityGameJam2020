﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bomb : MonoBehaviour
{
    public enum BombType { CROSS, ROUND }

    [Header("Attributes")]
    [SerializeField] BombType _bombType;
    [SerializeField] float _explodeTime;

    [Header("Explosion GameObjects")]
    [SerializeField] GameObject _crossProjectile;
    [SerializeField] GameObject _roundProjectile;

    private PlayerType _playerType;

    private void Start() {
        Invoke("Explode", _explodeTime);
    }

    public void SetType(PlayerType t) {
        _playerType = t;
    }

    void Explode() {
        if (_bombType == BombType.CROSS) {
            for (int i = 0; i < 4; i++) {
                var b = Instantiate(_crossProjectile, transform.position, Quaternion.identity);
                var transRot = b.transform;
                transRot.localEulerAngles = new Vector3(0, 0, (i*90));
                b.transform.localEulerAngles = transRot.localEulerAngles;
                b.GetComponent<BombExplosion>().SetBomb(BombType.CROSS);
                b.GetComponent<BombExplosion>().SetType(_playerType);
            }  
        }

        if (_bombType == BombType.ROUND) {
            var b = Instantiate(_roundProjectile, transform.position, Quaternion.identity);
            b.GetComponent<BombExplosion>().SetType(_playerType);
            b.GetComponent<BombExplosion>().SetBomb(BombType.ROUND);
        }

        Destroy(this.gameObject);
    }

}
