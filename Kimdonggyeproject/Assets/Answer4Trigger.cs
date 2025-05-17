using System.Collections;
using UnityEngine;

public class Answer4Trigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane 오브젝트 연결할 변수
    public GameObject Dial;
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;
    public Camera mainCamera;      // 카메라를 할당하거나, Start에서 찾기
    public float distance = 2.0f;  // 카메라로부터 떨어진 거리
    public DialogueControl Dialogue;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (!hasShownMessage)
            {
                pressFPlane.SetActive(true); // Plane을 보이게
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

            pressFPlane.SetActive(false); // 영역 벗어나면 다시 숨기기
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
        Debug.Log("Dial을 소환합니다."); //이 코드를 컷씬 진행으로 바꾸면 됨.
    }
}

