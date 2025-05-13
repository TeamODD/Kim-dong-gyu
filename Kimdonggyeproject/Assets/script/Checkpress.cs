using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위한 네임스페이스 (필요시)

public class PressAnyKey : MonoBehaviour
{
    private bool keyPressed = false;

    void Update()
    {
        if (!keyPressed && Input.anyKeyDown)
        {
            keyPressed = true;
            Debug.Log("Pressed Any Key!");

            // 여기에 원하는 동작 추가
            // 예: 다음 씬으로 전환
            // SceneManager.LoadScene("NextSceneName");

            // 혹은 UI 숨기기 등
            // gameObject.SetActive(false);
        }
    }
}
