using UnityEngine;

public class Answer1Trigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane ������Ʈ ������ ����
    public GameObject Puzzle1;
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
            Puzzle1.SetActive(true);
            Debug.Log("����1 ����"); // ���� ������ Ǯ���ٸ� currentActivateIndex++
            //Ŭ���� ���� if()
            //if()
            ObjectActivationManage.currentPuzzleIndex++;
        }
    }
}

