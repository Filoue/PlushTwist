using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float speed = 1f;
    public float pathZ = 0f;
    [Range(0.01f, 0.1f)]
    public float pathThreshold = 0.1f;
    bool isOnPath = false;

    private void FixedUpdate()
    {
        if (!isOnPath)
        {
            Vector3 dir = new Vector3(0,0,pathZ-transform.position.z).normalized;
            transform.position += dir * speed * Time.fixedDeltaTime;

            if (Mathf.Abs(transform.position.z - pathZ) < pathThreshold)
            {
                isOnPath = true;
            }
        }
        else
        {
            transform.position += Vector3.right * speed * Time.fixedDeltaTime;
        }
    }
}
