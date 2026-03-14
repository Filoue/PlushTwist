using UnityEngine;

public class GhostCursor : MonoBehaviour
{
    public GameObject cursor;
    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private Color originalColor;
    private GameManager gameManager;
    private void Start()
    {
        mesh = cursor.GetComponent<Mesh>();
        meshRenderer = cursor.GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the cursor stays on the same plane
        cursor.transform.position = mousePosition;
        // Change color based on whether the cursor is over a valid placement area
        if (IsOverValidPlacementArea())
        {
            meshRenderer.material.color = Color.green; // Valid placement
        }
        else
        {
            meshRenderer.material.color = Color.red; // Invalid placement
        }
    }
    private bool IsOverValidPlacementArea()
    {
        return gameManager.gridObjectsArray[(int)cursor.transform.position.x + gameManager.gridSizeX / 2, (int)cursor.transform.position.z + gameManager.gridSizeZ / 2] == null;
    }
}
