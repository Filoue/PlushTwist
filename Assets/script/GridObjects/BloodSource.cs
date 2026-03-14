using UnityEngine;

public class BloodSource : GridObject
{
    public bool isEmpty
    {
        get { return bloodAmount <= 0f; }
    }

    private void Update()
    {
        if (isEmpty)
        {
            // Visualize empty state (e.g., change color)
            GetComponent<Renderer>().material.color = Color.white;
            isSellable = true;
        }
        else
        {
            // Visualize non-empty state (e.g., change color)
            GetComponent<Renderer>().material.color = Color.black;
            isSellable = false;
        }
    }
}
