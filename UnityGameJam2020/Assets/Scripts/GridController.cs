using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public GameObject greenCell;
    public GameObject brownCell;
    private GameObject[] _grid = new GameObject[81];
    private GameObject[] _plant = new GameObject[16];
    private int _gridCount, _plantCount;
    public Transform max;
    private void Start()
    {
        CreateGrid();
        CreatePlantArea();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha7))
            SevenBySeven();
        if (Input.GetKey(KeyCode.Alpha5))
            FiveByFive();

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

    void SevenBySeven()
    {
        for(int i = 0; i < _gridCount; ++i)
        {
            if(_grid[i].transform.position.x >= 7 || _grid[i].transform.position.y >= 7)
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
    }
}
