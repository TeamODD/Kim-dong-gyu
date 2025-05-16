using UnityEngine;

public class Hint2_2Trigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane ������Ʈ ������ ����
    public GameObject PreParing_Postit;
    public GameObject PuzzleBackground;
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;
    private bool isPostItActive = false;

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
        
        
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && ObjectActivationManage.currentPuzzleIndex >= 3 && ObjectActivationManage.currentPuzzleIndex <= 4)
        {
            if (ObjectActivationManage.currentPuzzleIndex == 3)
            {
                ObjectActivationManage.currentPuzzleIndex++; //3�� ��쿡�� ���� -> Answer 2 Ȱ��ȭ
            }
            Debug.Log("���� �ε�����" + ObjectActivationManage.currentPuzzleIndex);
            PreParing_Postit.SetActive(true);
            isPostItActive = true;
        }
        if (PreParing_Postit.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PreParing_Postit.SetActive(false);
                isPostItActive = false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                PreParing_Postit.SetActive(false);
                isPostItActive = false;
            }

        }
        if (isPostItActive)
        {
            PuzzleBackground.SetActive(true);
        }
        else
        {
            PuzzleBackground.SetActive(false);
        }

    }
}

