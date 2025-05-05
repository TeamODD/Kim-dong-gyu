using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("hi!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (!hasShownMessage)
            {
                Debug.Log("Press F");
                hasShownMessage = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            hasShownMessage = false; // 다시 들어올 때 한 번 더 출력되도록 초기화
        }
    }
}
