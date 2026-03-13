using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;
    
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private TileBase tile;
    
    public GameObject prefab1;
    public GameObject prefab2;
    
    private PlaceableObjects objectToPlace;
    
    #region Unity methods

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    #endregion
    
    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        
        return position;
    }
    
    #endregion
    
    #region Building Placement

    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);
        
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace =  obj.GetComponent<PlaceableObjects>();
        obj.AddComponent<ObjectDrag>();
    }
    
    #endregion
    
    #region Input System

    public void BuildGraveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InitializeWithObject(prefab1);
        }
    }

    public void BuildPlantInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            InitializeWithObject(prefab2);
        }
    }
    
    #endregion
}
