using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject greenCell;
    public GameObject brownCell;
    public BombPlanter player1, player2;
    public GameObject bomb;
    private GameObject[] _grid = new GameObject[81];
    private GameObject[] _plant = new GameObject[16];
    public GameObject block;
    private int _gridCount, _plantCount;
    private Vector2 _bombPos;
    private bool _hasBomb = false;
    public Transform max;
    public event Action EventWipePlants = () => { };
    private RoundTimer _rTime;
    private RoundScoring _rScore;

    private void Start()
    {
        CreateGrid();
        CreatePlantArea();
        CreateBlocks();
        _rTime = FindObjectOfType<RoundTimer>();
        _rScore = FindObjectOfType<RoundScoring>();
        _rTime.RoundStart += OnRoundChange;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha7))
            SevenBySeven();
        if (Input.GetKey(KeyCode.Alpha5))
            FiveByFive();
        if(!_hasBomb)
        {
            StartCoroutine(CreateBomb());
        }
    }

    IEnumerator CreateBomb()
    {
        _hasBomb = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 5f));
        CheckBombPosition();
        GameObject obj = Instantiate(bomb, _bombPos, Quaternion.identity);
        _hasBomb = false;
    }

    void CheckBombPosition()
    {
        int x = UnityEngine.Random.Range(0, 9);
        int y = UnityEngine.Random.Range(0, 9);
        _bombPos = new Vector2(x, y);
        if (_bombPos.x % 2 == 1 && _bombPos.y % 2 == 1)
            CheckBombPosition();
    }

    void CreateGrid()
    {
        _gridCount = 0;
        for (int i = 0; i <= max.position.x; ++i)
        {
            for (int j = 0; j <= max.position.y; ++j)
            {
                _grid[_gridCount] = Instantiate(greenCell, new Vector2(i, j), Quaternion.identity);
                _grid[_gridCount].transform.parent = this.transform;
                ++_gridCount;
            }
        }
    }

    void CreatePlantArea()
    {
        _plantCount = 0;
        for (int i = 1; i <= max.position.x; i+=2)
        {
            for (int j = 1; j <= max.position.y; j+=2)
            {
                if(i%2==1 && j%2==1)
                {
                    _plant[_plantCount] = Instantiate(brownCell, new Vector2(i, j), Quaternion.identity);
                    _plant[_plantCount].transform.parent = this.transform;
                    ++_plantCount;
                }
            }
        }
    }

    void CreateBlocks()
    {
        for (int i = 1; i <= max.position.x; i += 2)
        {
            for (int j = 1; j <= max.position.y; j += 2)
            {
                if (i % 2 == 1 && j % 2 == 1)
                {
                    GameObject blocks = Instantiate(block, new Vector2(i, j), Quaternion.identity);
                    blocks.transform.parent = this.transform;
                }
            }
        }
    }

    public void OnRoundChange() {
        if (_rScore.roundNum == 1) {
            SevenBySeven();
        } else if (_rScore.roundNum >= 2) {
            FiveByFive();
        }
    }

    void SevenBySeven()
    {
        for (int i = 0; i < _gridCount; ++i)
        {
            if (_grid[i].transform.position.x >= 7 || _grid[i].transform.position.y >= 7)
            {
                _grid[i].SetActive(false);
            }
        }
        for (int i = 0; i < _plantCount; ++i)
        {
            if (_plant[i].transform.position.x >= 7 || _plant[i].transform.position.y >= 7)
            {
                _plant[i].SetActive(false);
            }
        }
        max.transform.position = new Vector2(6, 6);
        FindObjectOfType<CameraBehaviour>().UpdateCameraFocus(7);
        if (player1.playerType == PlayerType.NATURE)
        {
            player1.transform.position = transform.position;
            player1.GetComponent<PlayerController>().ResetPos();
        }
        if (player2.playerType == PlayerType.MAN)
        {
            player2.transform.position = max.transform.position;
            player2.GetComponent<PlayerController>().ResetPos();
        }
        EventWipePlants();
    }

    void FiveByFive()
    {
        for (int i = 0; i < _gridCount; ++i)
        {
            if (_grid[i].transform.position.x >= 5 || _grid[i].transform.position.y >= 5)
            {
                _grid[i].SetActive(false);
            }
        }
        for (int i = 0; i < _plantCount; ++i)
        {
            if (_plant[i].transform.position.x >= 5 || _plant[i].transform.position.y >= 5)
            {
                _plant[i].SetActive(false);
            }
        }
        max.transform.position = new Vector2(4, 4);
        FindObjectOfType<CameraBehaviour>().UpdateCameraFocus(5);
        if (player1.playerType == PlayerType.NATURE)
        {
            player1.transform.position = transform.position;
            player1.GetComponent<PlayerController>().ResetPos();
        }
        if (player2.playerType == PlayerType.MAN)
        {
            player2.transform.position = max.transform.position;
            player2.GetComponent<PlayerController>().ResetPos();
        }
        EventWipePlants();
    }
}
