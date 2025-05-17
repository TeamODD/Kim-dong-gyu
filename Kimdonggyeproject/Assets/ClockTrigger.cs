//using UnityEditor.Rendering;
using UnityEngine;

public class ClockTrigger : MonoBehaviour
{
    public GameObject pressFPlaneClock; // PlaneClcock ������Ʈ ������ ����
    private bool isPlayerNearby = false;
    private bool hasShownMessage = false;
    public GameObject puzzle;
    private Puzzle4 puzzle4;

    void Start()
    {
        puzzle4 = puzzle.GetComponent<Puzzle4>();
        if (puzzle4 == null)
            Debug.LogError("Puzzle4 ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�!");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            if (!hasShownMessage)
            {
                pressFPlaneClock.SetActive(true); // Plane�� ���̰�
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

            pressFPlaneClock.SetActive(false); // ���� ����� �ٽ� �����
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("���� ����");
            // Puzzle4 �ν��Ͻ��� ���� ������ ����

            puzzle4.isPlayingPuzzle4 = true;

        }

    }
}