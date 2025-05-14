using UnityEngine;

public class Hint3_2Trigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane 오브젝트 연결할 변수
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;

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
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("컷씬 진행"); //이 코드를 컷씬 진행으로 바꾸면 됨. 
        }
    }
}
