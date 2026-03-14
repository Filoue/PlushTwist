using System.Collections.Generic;
using UnityEngine;

public class BloodRoot : GridObject
{
    public float pumpSpeed = 10f;
    public float maxBloodAmount = 50f;
    public int range = 2;
    private GridObject bloodSource;

    private void Update()
    {
        if (bloodSource == null)
        {
            //Find blood source
            List<GridObject> neighbours = getNeighbours(range);
            foreach (GridObject neighbour in neighbours)
            {
                if (neighbour is BloodSource && !neighbour.GetComponent<BloodSource>().isEmpty)
                {
                    bloodSource = neighbour;
                    break;
                }
            }
        }
        else
        {
            // Check if the current blood source is empty
            if (bloodSource.GetComponent<BloodSource>().isEmpty)
            {
                bloodSource = null;
                return;
            }

            // Pump blood from the source
            if (bloodAmount < maxBloodAmount)
            {
                float bloodToPump = Mathf.Min(pumpSpeed * Time.deltaTime, maxBloodAmount - bloodAmount);
                float bloodAvailable = bloodSource.GetComponent<BloodSource>().bloodAmount;
                bloodToPump = Mathf.Min(bloodToPump, bloodAvailable);
                bloodAmount += bloodToPump;
                bloodSource.GetComponent<BloodSource>().bloodAmount -= bloodToPump;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(range * 2f + 1f, 0.1f, range * 2f + 1f));
    }
}
