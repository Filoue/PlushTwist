using UnityEngine;

public class BloodSource : GridObject
{
    public bool isEmpty
    {
        get { return bloodAmount <= 0f; }
    }
}
