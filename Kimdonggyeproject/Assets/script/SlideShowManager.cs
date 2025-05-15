using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlideShowManager : MonoBehaviour
{
    public Image slideshowImage;             // UI → Image로 만든 오브젝트
    public Sprite[] slideSprites;            // 보여줄 사진들
    public float slideDuration = 2f;         // 각 슬라이드 시간

    private int currentIndex = 0;

    public void StartSlideshow()
    {
        gameObject.SetActive(true); // 슬라이드쇼 매니저 오브젝트 활성화
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

        // 끝났으면 종료
        gameObject.SetActive(false);
        currentIndex = 0;
    }
}
