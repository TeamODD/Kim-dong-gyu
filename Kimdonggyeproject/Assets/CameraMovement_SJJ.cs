using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    private float cameraY;

    void Start()
    {
        cameraY = transform.position.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new Vector3(target.position.x, cameraY, target.position.z - 20);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}