using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int selectedItemId = 0;
    public int blood = 0;
    public TextMeshProUGUI bloodCounterTextMeshPro;
    public List<GridObject> gridObjects = new List<GridObject>();

    void Update()
    {
        // Calculate total blood amount from all BloodStorage objects
        blood = 0;
        foreach (GridObject gridObject in gridObjects)
        {
            if (gridObject is BloodStorage)
            {
                blood += (int)gridObject.bloodAmount;
            }
        }
        bloodCounterTextMeshPro.text = blood.ToString();
    }

    public void SelectItem(int id) {
        selectedItemId = id;
    }
}
