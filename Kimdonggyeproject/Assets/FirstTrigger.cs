using UnityEngine;

public class FirstTrigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane ������Ʈ ������ ����
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (ObjectActivationManage.currentPuzzleIndex == 0) 
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
    }

    private void OnTriggerExit(Collider other)
    {
        if(ObjectActivationManage.currentPuzzleIndex == 0)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerNearby = false;
                hasShownMessage = false;

                pressFPlane.SetActive(false); // ���� ����� �ٽ� �����
            }
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && ObjectActivationManage.currentPuzzleIndex == 0)
        {
            
            // ��� ������
            ObjectActivationManage.currentPuzzleIndex++; // 1�� ���� ���� ������Ʈ ������ : index++;
        }
    }
}