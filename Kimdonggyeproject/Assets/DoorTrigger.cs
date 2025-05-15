using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject pressFPlane;
    public SlideShowManager slideShowManager; // 새로 추가
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (!hasShownMessage)
            {
                pressFPlane.SetActive(true);
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
            pressFPlane.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            pressFPlane.SetActive(false); // 안내 메시지 숨기기
            slideShowManager.StartSlideshow(); // 슬라이드쇼 실행
        }
    }
}
