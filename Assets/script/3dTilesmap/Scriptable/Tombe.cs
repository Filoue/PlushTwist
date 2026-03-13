using UnityEngine;

[CreateAssetMenu(fileName = "Tombe", menuName = "In Game placment/Tombe")]
public class Tombe : ScriptableObject
{
    public GameObject prefabs;
    public string name;
    public string description;

    public float temporalyValue;
    public Vector3 position;
}
