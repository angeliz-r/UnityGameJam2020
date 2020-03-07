using UnityEngine;

public class BombItem : MonoBehaviour
{
    public GridController grid;

    private void OnEnable()
    {
        grid = FindObjectOfType<GridController>();
    }
    private void Update()
    {
        if(transform.position.x > grid.max.position.x || transform.position.y > grid.max.position.y)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            collision.GetComponent<BombPlanter>().SetHasBomb(true);
            Destroy(this.gameObject);
        }
    }
}
