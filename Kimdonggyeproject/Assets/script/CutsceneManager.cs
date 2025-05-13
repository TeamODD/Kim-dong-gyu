using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public Image startImage;           // 시작 일러스트
    public Image fadeImage;           // 페이드용 이미지
    public RawImage videoRawImage;    // 영상 출력용
    public VideoPlayer videoPlayer;

    private bool hasStarted = false;
    private bool hasEnded = false;
    private bool canExit = false;

    void Start()
    {
        startImage.gameObject.SetActive(true);
        fadeImage.gameObject.SetActive(true);
        videoRawImage.enabled = false;
        fadeImage.color = new Color(0, 0, 0, 0); // 투명
        videoPlayer.Stop();
    }

    void Update()
    {
        // 아무 키로 시작
        if (!hasStarted && Input.anyKeyDown)
        {
            hasStarted = true;
            StartCoroutine(PlayCutsceneWithFade());
        }

        // ESC 1: 영상 정지 (스킵)
        if (hasStarted && !hasEnded && Input.GetKeyDown(KeyCode.Escape))
        {
            SkipCutscene();
        }

        // 영상 정상 종료: ESC 기다림
        if (hasStarted && !hasEnded && !videoPlayer.isPlaying && videoPlayer.time > 0.1f)
        {
            hasEnded = true;
            HoldLastFrame(); // 페이드 없이 정지
        }

        // ESC 2: 다음 씬
        if (canExit && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(FadeToWhiteAndExit());
        }
    }

    void SkipCutscene()
    {
        videoPlayer.Pause(); // 정지(마지막 프레임 유지)
        hasEnded = true;
        HoldLastFrame();
    }

    void HoldLastFrame()
    {
        // 영상 마지막 장면 그대로 유지
        videoRawImage.enabled = true;
        canExit = true;
    }

    IEnumerator PlayCutsceneWithFade()
    {
        startImage.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(0, 1, 1f));  // 검정 페이드 인

        videoRawImage.enabled = true;
        videoPlayer.Play();
        yield return StartCoroutine(Fade(1, 0, 0.5f)); // 검정 페이드 아웃
    }

    IEnumerator FadeToWhiteAndExit()
    {
        yield return StartCoroutine(FadeColor(Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(0.5f);
        //SceneManager.LoadScene("stage0");
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
