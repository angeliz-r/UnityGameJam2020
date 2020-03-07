using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    [SerializeField] RuntimeAnimatorController _crossManAnim;
    [SerializeField] RuntimeAnimatorController _roundManAnim;
    [SerializeField] RuntimeAnimatorController _crossNatureAnim;
    [SerializeField] RuntimeAnimatorController _roundNatureAnim;

    private PlayerType _type;
    private Bomb.BombType _bombType;
    private RoundScoring _rScore;
    private Animator _myAnim;

    private void OnEnable() {
        Invoke("DestroySelf", 1);
    }

    private void Start() {
        _rScore = FindObjectOfType<RoundScoring>();
        _myAnim = GetComponent<Animator>();

        if (_bombType == Bomb.BombType.CROSS && _type == PlayerType.MAN)
            _myAnim.runtimeAnimatorController = _crossManAnim;
        else if (_bombType == Bomb.BombType.CROSS && _type == PlayerType.NATURE)
            _myAnim.runtimeAnimatorController = _crossNatureAnim;
        else if (_bombType == Bomb.BombType.ROUND && _type == PlayerType.MAN)
            _myAnim.runtimeAnimatorController = _roundManAnim;
        else if (_bombType == Bomb.BombType.ROUND && _type == PlayerType.NATURE)
            _myAnim.runtimeAnimatorController = _roundNatureAnim;
    }

    private void FixedUpdate() {
        if (_bombType == Bomb.BombType.CROSS) {
            if(transform.localEulerAngles.z == 0 || transform.localEulerAngles.z == 90)
                transform.Translate(transform.right);
            else
                transform.Translate(transform.up);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Plant"))
        {
            var type = collision.GetComponent<Plants>().myPlant;
            if (_type != type)
            {
                collision.transform.parent.GetComponent<Collider2D>().enabled = true;
                if (type == PlayerType.MAN)
                {
                    _rScore.manScore.DestroyLiveScore();
                }
                else if (type == PlayerType.NATURE)
                { 
                    _rScore.natureScore.DestroyLiveScore();
                }
                Destroy(collision.gameObject);
            }
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().stunValue = 2f;
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
