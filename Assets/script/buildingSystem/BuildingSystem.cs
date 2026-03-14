using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public GridLayout gridLayout;
    public LayerMask placementLayerMask;
    private Grid grid;

    private static Vector3 mousePosition;
    private GameManager gameManager;

    private void Start()
    {
        grid = gridLayout.gameObject.GetComponent<Grid>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Don't do anything if the mouse is outside the screen
        if (mousePosition.x < 0 || mousePosition.x > Screen.width || mousePosition.y < 0 || mousePosition.y > Screen.height)
        {
            return Vector3.zero;
        }

        // Don't do anything if the mouse is on UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return Vector3.zero;
        }

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, placementLayerMask))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        
        return position;
    }

    private void InitializeWithObject(GameObject prefab, Vector3 position)
    {
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
    }

    public void Place(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GetMouseWorldPosition() == Vector3.zero) return;
            Vector3 position = SnapCoordinateToGrid(GetMouseWorldPosition());
            if (position == Vector3.zero) return;
            else if (gameManager.gridObjectsArray[(int)position.x + gameManager.gridSizeX / 2, (int)position.z + gameManager.gridSizeZ / 2] == null)
            {
                if (gameManager.availableBlood >= gameManager.selectedPrefab.GetComponent<GridObject>().price)
                {
                    InitializeWithObject(gameManager.selectedPrefab, position);
                    gameManager.RemoveBlood(gameManager.selectedPrefab.GetComponent<GridObject>().price);
                }
            }
        }
    }

    public void Remove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GetMouseWorldPosition() == Vector3.zero) return;
            Vector3 position = SnapCoordinateToGrid(GetMouseWorldPosition());
            if (position == Vector3.zero) return;
            else if (gameManager.gridObjectsArray[(int)position.x + gameManager.gridSizeX / 2, (int)position.z + gameManager.gridSizeZ / 2] != null)
            {
                GridObject gridObject = gameManager.gridObjectsArray[(int)position.x + gameManager.gridSizeX / 2, (int)position.z + gameManager.gridSizeZ / 2];
                gameManager.AddBlood(gridObject.price);
                Destroy(gridObject.gameObject);
            }
        }
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
}
