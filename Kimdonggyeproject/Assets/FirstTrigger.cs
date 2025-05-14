using UnityEngine;

public class FirstTrigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane ������Ʈ ������ ����
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (ObjectActivationManage.ActiveIndex == 0) 
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
        if(ObjectActivationManage.ActiveIndex == 0)
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
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && ObjectActivationManage.ActiveIndex == 0)
        {
            
            // ��� ������
            ObjectActivationManage.ActiveIndex++; // 1�� ���� ���� ������Ʈ ������ : index++;
        }
    }
}