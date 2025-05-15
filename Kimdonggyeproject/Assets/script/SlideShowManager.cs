using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlideShowManager : MonoBehaviour
{
    public Image slideshowImage;             // UI �� Image�� ���� ������Ʈ
    public Sprite[] slideSprites;            // ������ ������
    public float slideDuration = 2f;         // �� �����̵� �ð�

    private int currentIndex = 0;

    public void StartSlideshow()
    {
        gameObject.SetActive(true); // �����̵�� �Ŵ��� ������Ʈ Ȱ��ȭ
        StartCoroutine(PlaySlides());
    }

    private IEnumerator PlaySlides()
    {
        while (currentIndex < slideSprites.Length)
        {
            slideshowImage.sprite = slideSprites[currentIndex];
            currentIndex++;
            yield return new WaitForSeconds(slideDuration);
        }

        // �������� ����
        gameObject.SetActive(false);
        currentIndex = 0;
    }
}
