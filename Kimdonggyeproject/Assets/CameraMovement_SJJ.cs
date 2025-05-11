using UnityEngine;

public class CameraMovement_SJJ : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    private float cameraY;
    public GameObject puzzle;
    private Puzzle4 puzzle4;

    void Start()
    {
        puzzle4 = puzzle.GetComponent<Puzzle4>();
        cameraY = transform.position.y;
    }

    void LateUpdate()
    {
        if (puzzle4.isPlayingPuzzle4)
        {
            //Debug.Log("체크!");
            // Dial 오브젝트가 카메라 앞으로 이동.
        }
        else
        {
            if (target == null) return;

            Vector3 desiredPosition = new Vector3(target.position.x, cameraY, target.position.z - 20);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
