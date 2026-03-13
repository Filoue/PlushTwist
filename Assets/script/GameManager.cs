using UnityEngine;
using Unity.UI.Text;

public class GameManager : MonoBehaviour
{
    public int selectedItemId = 0;
    public int bood = 0;
    public Text bloodCounterText = 0;

    void Update()
    {
        
    }

    public void SelectItem(int id) {
        selectedItemId = id;
    }
}
