using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject pressFPlane;
    public CutScene2 cutsceneManager;

    private bool isPlayerNearby = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            pressFPlane.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            pressFPlane.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("컷씬 실행");
            cutsceneManager.PlayCutscene(); // 이제 정상적으로 호출됨!
            pressFPlane.SetActive(false); // UI 숨기기
        }
    }
}
