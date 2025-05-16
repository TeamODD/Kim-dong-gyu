using Unity.VisualScripting;
using UnityEngine;

public class Hint2Trigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane 오브젝트 연결할 변수
    public GameObject Diary1;
    public GameObject Diary2;
    public GameObject PuzzleBackground;
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;
    private bool isDiaryActive = false;

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
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && (ObjectActivationManage.currentPuzzleIndex >= 2) && (ObjectActivationManage.currentPuzzleIndex <= 4))
        {
            if (ObjectActivationManage.currentPuzzleIndex == 2)
            {
                ObjectActivationManage.currentPuzzleIndex++; // hint2_2 활성화 -> hint2_2가 활성화 되어 있을 때는 index 증가 안함
            }
            Debug.Log("현재 인덱스는" + ObjectActivationManage.currentPuzzleIndex);
            if (isDiaryActive == false)
            {
                Diary1.SetActive(true);
                Diary2.SetActive(true);
                isDiaryActive = true; //일기장 구현
            }
                  
        }
        if (isDiaryActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Diary1.SetActive(false);
                Diary2.SetActive(false);
                isDiaryActive = false;
            }
            if (Input.GetMouseButtonDown(0)) //좌클릭
            {
                if (Diary1.activeSelf)
                {
                    Diary1.SetActive(false);
                }
                else if (Diary2.activeSelf) 
                {
                    Diary2.SetActive(false);
                    isDiaryActive = false;
                }
            }
        }
        
        if (isDiaryActive)
        {
            PuzzleBackground.SetActive(true);
        }
        else
        {
            PuzzleBackground.SetActive(false);
        }
    }
 }

