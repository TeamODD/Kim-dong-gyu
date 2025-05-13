using UnityEditor.Rendering;
using UnityEngine;

public class ClockTrigger : MonoBehaviour
{
    public GameObject pressFPlaneClock; // PlaneClcock 오브젝트 연결할 변수
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;
    public GameObject puzzle;
    private Puzzle4 puzzle4;

    void Start()
    {
        puzzle4 = puzzle.GetComponent<Puzzle4>();
        if (puzzle4 == null)
            Debug.LogError("Puzzle4 컴포넌트가 할당되지 않았습니다!");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (!hasShownMessage)
            {
                pressFPlaneClock.SetActive(true); // Plane을 보이게
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

            pressFPlaneClock.SetActive(false); // 영역 벗어나면 다시 숨기기
        }
    }

        private void Update()
        {
            if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("퍼즐 진행");
                // Puzzle4 인스턴스를 통해 변수에 접근
                
                puzzle4.isPlayingPuzzle4 = true;
                
            }

        }
    }

