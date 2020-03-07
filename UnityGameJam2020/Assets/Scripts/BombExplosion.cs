using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    private PlayerType _type;
    private Bomb.BombType _bombType;

    private void OnEnable() {
        Invoke("DestroySelf", 1);
    }

    private void Update() {
        if (_bombType == Bomb.BombType.CROSS) {
            if(transform.localEulerAngles.z == 0 || transform.localEulerAngles.z == 90)
                transform.Translate(transform.right);
            else
                transform.Translate(transform.up);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Plant"))
        {
            if (_type != collision.GetComponent<Plants>().myPlant)
            {
                collision.transform.parent.GetComponent<Collider2D>().enabled = true;
                Destroy(collision.gameObject);
            }
        }
    }

    void DestroySelf() {
        Destroy(this.gameObject);
    }

    public void SetType(PlayerType t) {
        _type = t;
    }

    public void SetBomb(Bomb.BombType t) {
        _bombType = t;
    }
 
}
