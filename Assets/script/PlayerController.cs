using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


    public class PlayerController : MonoBehaviour
    {
       [SerializeField] private float speed = 10f;
        private Vector2 inputDirection;
        private Camera cam;

        public void OnMove(InputAction.CallbackContext context)
        {
            inputDirection = context.ReadValue<Vector2>();
        }

        void Start()
        {
            cam =  Camera.main;
        }
        void Update()
        {
            Vector3 move = new Vector3(inputDirection.x, 0, inputDirection.y) * speed * Time.deltaTime;
        
            cam.transform.Translate(move, Space.World);
        }
    }
