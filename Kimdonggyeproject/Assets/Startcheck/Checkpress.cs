using UnityEngine;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� ���ӽ����̽� (�ʿ��)

public class PressAnyKey : MonoBehaviour
{
    private bool keyPressed = false;

    void Update()
    {
        if (!keyPressed && Input.anyKeyDown)
        {
            keyPressed = true;
            Debug.Log("Pressed Any Key!");

            // ���⿡ ���ϴ� ���� �߰�
            // ��: ���� ������ ��ȯ
            // SceneManager.LoadScene("NextSceneName");

            // Ȥ�� UI ����� ��
            // gameObject.SetActive(false);
        }
    }
}
