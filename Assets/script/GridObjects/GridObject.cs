using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    public float bloodAmount = 0f;
    public List<GridObject> getNeighbours(int range)
    {
        return new List<GridObject>();
    }

    private void Start()
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.gridObjects.Add(this);
    }
}
