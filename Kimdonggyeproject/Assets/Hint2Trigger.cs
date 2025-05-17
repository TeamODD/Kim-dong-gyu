using Unity.VisualScripting;
using UnityEngine;

public class Hint2Trigger : MonoBehaviour
{
    public GameObject pressFPlane; // Plane ������Ʈ ������ ����
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
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && (ObjectActivationManage.currentPuzzleIndex >= 2) && (ObjectActivationManage.currentPuzzleIndex <= 4))
        {
            if (ObjectActivationManage.currentPuzzleIndex == 2)
            {
                ObjectActivationManage.currentPuzzleIndex++; // hint2_2 Ȱ��ȭ -> hint2_2�� Ȱ��ȭ �Ǿ� ���� ���� index ���� ����
            }
            Debug.Log("���� �ε�����" + ObjectActivationManage.currentPuzzleIndex);
            if (isDiaryActive == false)
            {
                Diary1.SetActive(true);
                Diary2.SetActive(true);
                isDiaryActive = true; //�ϱ��� ����
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
            if (Input.GetMouseButtonDown(0)) //��Ŭ��
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

