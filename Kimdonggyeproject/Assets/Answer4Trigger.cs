using System.Collections;
using UnityEngine;

public class Answer4Trigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane ������Ʈ ������ ����
    public GameObject Dial;
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;
    public Camera mainCamera;      // ī�޶� �Ҵ��ϰų�, Start���� ã��
    public float distance = 2.0f;  // ī�޶�κ��� ������ �Ÿ�
    public DialogueControl Dialogue;
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
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && ObjectActivationManage.currentPuzzleIndex == 9)
        {
            StartCoroutine(ShowText_Coroutine());
        }

    }
    private IEnumerator ShowText_Coroutine()
    {
        Dialogue.ShowText("hello world!");
        yield return StartCoroutine(Dialogue.TextEnter_Coroutine());
        Dial.SetActive(true);
        ObjectActivationManage.isPlayingPuzzle = true;
        Vector3 positionInFront = mainCamera.transform.position + mainCamera.transform.forward * distance;
        Debug.Log("Dial�� ��ȯ�մϴ�."); //�� �ڵ带 �ƾ� �������� �ٲٸ� ��.
    }
}

