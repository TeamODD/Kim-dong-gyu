using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public Image startImage;
    public Image fadeImage;
    public RawImage videoRawImage;
    public VideoPlayer videoPlayer;
    public Image endingPhotoImage; 


    private bool hasStarted = false;
    private bool hasEnded = false;
    private bool canExit = false;

    private float exitCooldown = 0f; // ESC 연타 방지용

    void Start()
    {
        startImage.gameObject.SetActive(true);
        fadeImage.gameObject.SetActive(true);
        videoRawImage.enabled = false;
        fadeImage.color = new Color(0, 0, 0, 0);
        videoPlayer.Stop();
        endingPhotoImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!hasStarted && Input.anyKeyDown)
        {
            hasStarted = true;
            StartCoroutine(PlayCutsceneWithFade());
        }

        // ESC 첫 입력 → 영상 정지 (멈춘 장면 보여줌)
        if (hasStarted && !hasEnded && Input.GetKeyDown(KeyCode.Escape) && exitCooldown <= 0f)
        {
            hasEnded = true;
            StopAndShowLastFrame();
            canExit = true;
            exitCooldown = 0.3f; // 잠시 ESC 입력 무시
        }

        // 영상 정상 종료 시
        if (hasStarted && !hasEnded && !videoPlayer.isPlaying && videoPlayer.time > 0.1f)
        {
            hasEnded = true;
            StopAndShowLastFrame();
            canExit = true;
        }

        // ESC 두 번째 입력 → 씬 전환
        if (canExit && Input.GetKeyDown(KeyCode.Escape) && exitCooldown <= 0f)
        {
            StartCoroutine(FadeToWhiteAndExit());
        }

        // ESC 입력 쿨다운 감소
        if (exitCooldown > 0f)
        {
            exitCooldown -= Time.deltaTime;
        }
    }

    IEnumerator PlayCutsceneWithFade()
    {
        startImage.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(0, 1, 1f));

        videoRawImage.enabled = true;
        videoPlayer.Play();
        yield return StartCoroutine(Fade(1, 0, 0.5f));
    }

    void StopAndShowLastFrame()
    {
        videoPlayer.frame = (long)videoPlayer.frameCount - 1;
        videoPlayer.Pause();
        videoRawImage.enabled = true;

        // 사진 띄우기
        if (endingPhotoImage != null)
        {
            endingPhotoImage.gameObject.SetActive(true);
        }
    }

    IEnumerator FadeToWhiteAndExit()
    {
        // 사진 숨기기
        if (endingPhotoImage != null)
            endingPhotoImage.gameObject.SetActive(false);

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
