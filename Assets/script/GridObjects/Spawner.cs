using System.Collections.Generic;
using UnityEngine;

public class Spawner : GridObject
{
    public GameObject prefabToSpawn;
    public float spawnPrice = 100f;
    public float spawnInterval = 5f;
    private float timeSinceLastSpawn = 0f;
    [SerializeField]
    private List<BloodStorage> bloodStorages = new List<BloodStorage>();
    private float availableBloodAmount = 0f;

    private void Update()
    {
        CheckStorages();

        timeSinceLastSpawn += Time.deltaTime;

        // Check if we can spawn an object
        if (timeSinceLastSpawn >= spawnInterval && availableBloodAmount >= spawnPrice)
        {
            SpawnObject();
            timeSinceLastSpawn = 0f;

            float priceToPay = spawnPrice;
            foreach (BloodStorage storage in bloodStorages)
            {
                if (storage.bloodAmount >= priceToPay)
                {
                    storage.RemoveBlood(priceToPay);
                    break;
                }
                else
                {
                    priceToPay -= storage.bloodAmount;
                    storage.RemoveBlood(storage.bloodAmount);
                }
            }
        }
    }

    private void CheckStorages()
    {
        // Update list of nearby blood storages
        List<GridObject> neighbours = getNeighbours(5);
        foreach (GridObject obj in neighbours)
        {
            if (obj is BloodStorage storage)
            {
                bloodStorages.Add(storage);
            }
        }

        bloodStorages.RemoveAll(storage => !neighbours.Contains(storage));

        // Calculate available blood amount
        availableBloodAmount = 0f;
        foreach (BloodStorage storage in bloodStorages)
        {
            availableBloodAmount += storage.bloodAmount;
        }
    }
    
    private void SpawnObject()
    {
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
    }
}
