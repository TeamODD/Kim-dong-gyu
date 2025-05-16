using UnityEngine;

public class Answer1Trigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane ������Ʈ ������ ����
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (!hasShownMessage)
            {
                pressFPlane.SetActive(true); // Plane�� ���̰�
                hasShownMessage = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            hasShownMessage = false;

            pressFPlane.SetActive(false); // ���� ����� �ٽ� �����
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && ObjectActivationManage.currentPuzzleIndex == 1)
        {

            Debug.Log("����1 ����"); //�� �ڵ带 �ƾ� �������� �ٲٸ� ��.
            
        }
    }
}

