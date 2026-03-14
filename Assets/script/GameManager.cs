using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float blood = 50;
    public float availableBlood { get { return blood; } }
    public TextMeshProUGUI bloodCounterTextMeshPro;

    public GameObject selectedPrefab;
    public List<GameObject> prefabs = new List<GameObject>();

    public int gridSizeX = 10, gridSizeZ = 10;
    public GridObject[,] gridObjectsArray;

    private void Awake()
    {
        gridObjectsArray = new GridObject[gridSizeX, gridSizeZ];
    }
    private void Start()
    {
        selectedPrefab = prefabs[0];
    }

    void Update()
    {
        // Calculate total blood amount from all BloodStorage objects
        blood = 0;
        foreach (GridObject gridObject in gridObjectsArray)
        {
            if (gridObject is BloodStorage)
            {
                blood += (int)gridObject.bloodAmount;
            }
        }
        bloodCounterTextMeshPro.text = ((int)blood).ToString();
    }

    public void SelectItem(int id) {
        selectedPrefab = prefabs[id];
    }

    public void RemoveBlood(float amount)
    {
        foreach (GridObject gridObject in gridObjectsArray)
        {
            if (gridObject is BloodStorage)
            {
                BloodStorage bloodStorage = gridObject as BloodStorage;
                float storageBlood = bloodStorage.bloodAmount;
                float bloodToRemoveFromStorage = Mathf.Min(storageBlood, amount);
                bloodStorage.RemoveBlood(bloodToRemoveFromStorage);
                amount -= bloodToRemoveFromStorage;
                if (amount <= 0f)
                {
                    break;
                }
            }
        }
    }

    public void AddBlood(float amount)
    {
        foreach (GridObject gridObject in gridObjectsArray)
        {
            if (gridObject is BloodStorage)
            {
                BloodStorage bloodStorage = gridObject as BloodStorage;
                float storageBlood = bloodStorage.bloodAmount;
                float bloodToAddToStorage = Mathf.Min(bloodStorage.maxBloodAmount - storageBlood, amount);
                bloodStorage.AddBlood(bloodToAddToStorage);
                amount -= bloodToAddToStorage;
                if (amount <= 0f)
                {
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(gridSizeX, 0.1f, gridSizeZ));
    }
}
