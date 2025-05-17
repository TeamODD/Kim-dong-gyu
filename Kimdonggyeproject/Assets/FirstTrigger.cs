using UnityEngine;

public class FirstTrigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane 오브젝트 연결할 변수
    public GameObject Stage2Plane; // 배경 오버랩 변수
    public GameObject StageEmptyPlane;
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

    private void Start()
    {
        Stage2Plane.SetActive(false);
        StageEmptyPlane.SetActive(true);
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            // 서서히 나타나게 (페이드인 되려나)
            Stage2Plane.SetActive(true);
            StageEmptyPlane.SetActive(false);
            Debug.Log("현재 인덱스는 "+ ObjectActivationManage.currentPuzzleIndex);
            ObjectActivationManage.currentPuzzleIndex++; // 1번 퍼즐 관련 오브젝트 오버랩 : index++;
            
        }
    }
}