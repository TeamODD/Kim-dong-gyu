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
            hasShownMessage = false; // �ٽ� ���� �� �� �� �� ��µǵ��� �ʱ�ȭ
        }
    }
}
