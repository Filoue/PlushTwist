using System.Collections.Generic;
using UnityEngine;

public class BloodStorage : GridObject
{
    public float pumpSpeed = 40f;
    public float maxBloodAmount = 100f;
    public int range = 3;
    private List<BloodRoot> connectedBloodRoots = new List<BloodRoot>();

    private void Update()
    {
        CheckBloodRoots();

        // Transfer blood from connected BloodRoots to this BloodStorage
        foreach (BloodRoot bloodRoot in connectedBloodRoots)
        {
            if (bloodAmount < maxBloodAmount)
            {
                float bloodToPump = Mathf.Min(pumpSpeed * Time.deltaTime, maxBloodAmount - bloodAmount);
                float bloodAvailable = bloodRoot.bloodAmount;
                bloodToPump = Mathf.Min(bloodToPump, bloodAvailable);
                bloodAmount += bloodToPump;
                bloodRoot.bloodAmount -= bloodToPump;
            }
            else
            {
                break;
            }
        }
    }

    private void CheckBloodRoots()
    {
        List<GridObject> neighbours = getNeighbours(range);
        foreach (GridObject neighbour in neighbours)
        {
            BloodRoot bloodRoot = neighbour.GetComponent<BloodRoot>();
            if (bloodRoot != null && !connectedBloodRoots.Contains(bloodRoot))
            {
                connectedBloodRoots.Add(bloodRoot);
            }
        }

        connectedBloodRoots.RemoveAll(root => !neighbours.Contains(root));
    }

    public void AddBlood(float amount)
    {
        bloodAmount = Mathf.Min(bloodAmount + amount, maxBloodAmount);
    }
    public void RemoveBlood(float amount)
    {
        bloodAmount = Mathf.Max(bloodAmount - amount, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(range * 2f + 1f, 0.1f, range * 2f + 1f));
    }
}
