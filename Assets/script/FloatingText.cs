using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float lifetime = 0.5f;
    public float floatSpeed = 1f;

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * floatSpeed * Time.fixedDeltaTime;
    }
}
