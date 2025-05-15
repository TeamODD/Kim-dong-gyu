using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public Image startImage;
    public Image fadeImage;
    public Image slideshowImage; // 슬라이드쇼에 사용할 UI 이미지
    public Sprite[] cutsceneImages; // 보여줄 컷씬 이미지들

    public float slideDuration = 2.5f; // 한 장당 보여주는 시간

    private bool hasStarted = false;
    private bool hasEnded = false;
    private bool canExit = false;
    private float exitCooldown = 0f;

    private int currentSlide = 0;
    private Coroutine slideshowCoroutine;

    void Start()
    {
        startImage.gameObject.SetActive(true);
        fadeImage.gameObject.SetActive(true);
        slideshowImage.enabled = false;
        fadeImage.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (!hasStarted && Input.anyKeyDown)
        {
            hasStarted = true;
            StartCoroutine(StartSlideshow());
        }

        if (hasStarted && !hasEnded && Input.GetKeyDown(KeyCode.Escape) && exitCooldown <= 0f)
        {
            hasEnded = true;
            if (slideshowCoroutine != null)
                StopCoroutine(slideshowCoroutine);
            ShowLastSlide();
            canExit = true;
            exitCooldown = 0.3f;
        }

        if (canExit && Input.GetKeyDown(KeyCode.Escape) && exitCooldown <= 0f)
        {
            StartCoroutine(FadeToWhiteAndExit());
        }

        if (exitCooldown > 0f)
        {
            exitCooldown -= Time.deltaTime;
        }
    }

    IEnumerator StartSlideshow()
    {
        startImage.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(0, 1, 1f));

        slideshowImage.enabled = true;
        yield return StartCoroutine(Fade(1, 0, 0.5f));

        slideshowCoroutine = StartCoroutine(PlaySlides());
    }

    IEnumerator PlaySlides()
    {
        while (currentSlide < cutsceneImages.Length)
        {
            slideshowImage.sprite = cutsceneImages[currentSlide];
            currentSlide++;
            yield return new WaitForSeconds(slideDuration);
        }

        // 다 끝나면 멈춤 상태로
        hasEnded = true;
        canExit = true;
    }

    void ShowLastSlide()
    {
        slideshowImage.sprite = cutsceneImages[cutsceneImages.Length - 1];
    }

    IEnumerator FadeToWhiteAndExit()
    {
        yield return StartCoroutine(FadeColor(Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Stage0");
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, to);
    }

    IEnumerator FadeColor(Color from, Color to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            fadeImage.color = Color.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = to;
    }
}
