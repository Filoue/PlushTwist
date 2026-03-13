using System.Collections.Generic;
using UnityEngine;

public class BloodStorage : GridObject
{
    public float maxBloodAmount = 100f;
    [SerializeField]
    private List<BloodRoot> connectedBloodRoots = new List<BloodRoot>();

    private void Update()
    {
        List<GridObject> neighbours = getNeighbours(3);
        foreach (GridObject neighbour in neighbours)
        {
            BloodRoot bloodRoot = neighbour.GetComponent<BloodRoot>();
            if (bloodRoot != null && !connectedBloodRoots.Contains(bloodRoot))
            {
                connectedBloodRoots.Add(bloodRoot);
            }
        }

        //connectedBloodRoots.RemoveAll(root => !neighbours.Contains(root));

        foreach (BloodRoot bloodRoot in connectedBloodRoots)
        {
            if (bloodAmount < maxBloodAmount)
            {
                float bloodToTransfer = Mathf.Min(bloodRoot.bloodAmount, maxBloodAmount - bloodAmount);
                bloodAmount += bloodToTransfer;
                bloodRoot.bloodAmount -= bloodToTransfer;
            }
            else
            {
                break;
            }
        }
    }

    public void AddBlood(float amount)
    {
        bloodAmount = Mathf.Min(bloodAmount + amount, maxBloodAmount);
    }
    public void RemoveBlood(float amount)
    {
        bloodAmount = Mathf.Max(bloodAmount - amount, 0f);
    }
}
