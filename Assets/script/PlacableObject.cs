using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlacableObject : MonoBehaviour
{
    [Header("Placable Object")]
    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private Vector3Int position;
    
    [Header("player")]
    [SerializeField] private float speed;
    [SerializeField] private LayerMask layerMask;
    
    private Vector2 mousePosition;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    public void OnMousePos(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            PerformRayCast();
        }
    }

    private void PerformRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            Debug.Log($"Hit {hit.transform.name} at {hit.point}");
            
            Instantiate(prefabs[0], new Vector3Int((int)hit.point.x, 0, (int)hit.point.z), Quaternion.identity);
        }
    }

    void Update()
    {
        //Debug.Log($"Mouse Screen Position: {mousePosition}");
    }
}
