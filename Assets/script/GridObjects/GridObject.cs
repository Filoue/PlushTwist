using System.Collections.Generic;
using UnityEngine;

public abstract class GridObject : MonoBehaviour
{
    private GameManager gameManager;
    public float price = 0f;
    public float bloodAmount = 0f;
    public List<GridObject> getNeighbours(int range)
    {
        List<GridObject> neighbours = new List<GridObject>();

        for (int x = -range; x <= range; x++)
        {
            for (int z = -range; z <= range; z++)
            {
                if (x == 0 && z == 0) continue; // Skip the current object
                int neighbourX = (int)transform.position.x + x + gameManager.gridSizeX / 2;
                int neighbourZ = (int)transform.position.z + z + gameManager.gridSizeZ / 2;
                // Check if the neighbour is within the bounds of the grid
                if (neighbourX >= 0 && neighbourX < gameManager.gridSizeX &&
                    neighbourZ >= 0 && neighbourZ < gameManager.gridSizeZ)
                {
                    GridObject neighbour = gameManager.gridObjectsArray[neighbourX, neighbourZ];
                    if (neighbour != null)
                    {
                        neighbours.Add(neighbour);
                    }
                }
            }
        }

        return neighbours;
    }

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.gridObjectsArray[(int)transform.position.x + gameManager.gridSizeX / 2, (int)transform.position.z + gameManager.gridSizeZ / 2] = this;
    }

    private void OnDestroy()
    {
        gameManager.gridObjectsArray[(int)transform.position.x + gameManager.gridSizeX / 2, (int)transform.position.z + gameManager.gridSizeZ / 2] = null;
    }
}
