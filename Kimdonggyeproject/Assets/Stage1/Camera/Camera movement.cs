using UnityEngine;
using System.Threading;
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public float smoothChange = 1000f;
    private float cameraY;
    private int isStarted = 1;
    public Camera cam;
    PlayerMovementRB playersc;
    public GameObject target_object;

    void Start()
    {
        cameraY = transform.position.y;
        playersc = target_object.GetComponent<PlayerMovementRB>();
    }

    void FixedUpdate()
    {
        if (target == null) return;
        // if(isStarted == -1){
        //     isStarted = 0;
        //     Thread.Sleep(3000);
        //     isStarted = 1;
        //     return;
        // }else if(isStarted == 0)
        //     return;

        Vector3 desiredPosition = new Vector3(target.position.x, cameraY, target.position.z - 14);
        if(cam.orthographicSize > 4 && isStarted == 1){
            cam.orthographicSize -= 14.0f / smoothChange;
            smoothSpeed += 4f / smoothChange / 1000f;
        }
        else if(isStarted == 1){
            isStarted = 2;
            playersc.canMove = true;
            smoothSpeed = 5f;
        }
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}
