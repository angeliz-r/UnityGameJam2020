using System.Collections;
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
            // Spawn projectile in 4 directions that fly there.
        }

        if (_bombType == BombType.ROUND) {
            // Spawn a large 3x3 area explosion
        }

        Destroy(this.gameObject);
    }

}
