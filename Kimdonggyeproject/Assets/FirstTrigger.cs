using UnityEngine;

public class FirstTrigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane 오브젝트 연결할 변수
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
                    pressFPlane.SetActive(true); // Plane을 보이게
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

                pressFPlane.SetActive(false); // 영역 벗어나면 다시 숨기기
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
            
            // 배경 오버랩
            ObjectActivationManage.ActiveIndex++; // 1번 퍼즐 관련 오브젝트 오버랩 : index++;
        }
    }
}