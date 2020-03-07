using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public PlayerType myPlant;
    public GridController grid;

    private void OnEnable()
    {
        grid = FindObjectOfType<GridController>();
        grid.EventWipePlants += DestroyPlants;
    }
    private void Start()
    {
        grid = FindObjectOfType<GridController>();
       
    }

    private void OnDestroy() {
        grid.EventWipePlants -= DestroyPlants;
    }
    void DestroyPlants()
    {
        grid.EventWipePlants -= DestroyPlants;
        Destroy(this.gameObject);
    }
}
