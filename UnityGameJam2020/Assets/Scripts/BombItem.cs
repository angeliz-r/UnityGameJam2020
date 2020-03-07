using UnityEngine;

public class BombItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<BombPlanter>().SetHasBomb(true);
            Destroy(this.gameObject);
        }
    }
}
