using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    public GameObject pressFPlane;       // F키 누르라는 UI
    public Image fadeImage;              // 페이드용 이미지 (검정색)
    public RawImage videoRawImage;       // 영상 띄울 RawImage
    public VideoPlayer videoPlayer;      // 영상 플레이어
    public Image endingPhotoImage;       // 영상 끝나고 띄울 사진
    public Image wasd;

    private bool isPlayerNearby = false;
    private bool hasStarted = false;
    private bool hasEnded = false;
    private bool canExit = false;
    private float exitCooldown = 0f;

    void Start()
    {
        pressFPlane.SetActive(false);
        fadeImage.gameObject.SetActive(true);
        videoRawImage.enabled = false;
        fadeImage.color = new Color(0, 0, 0, 0);
        videoPlayer.Stop();
        endingPhotoImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && !hasStarted && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(PlayCutsceneWithFade());
        }

        if (hasStarted && !hasEnded && Input.GetKeyDown(KeyCode.Escape) && exitCooldown <= 0f)
        {
            hasEnded = true;
            StopAndShowLastFrame();
            canExit = true;
            exitCooldown = 0.3f;
        }

        if (hasStarted && !hasEnded && !videoPlayer.isPlaying && videoPlayer.time > 0.1f)
        {
            hasEnded = true;
            StopAndShowLastFrame();
            canExit = true;
        }

        if (canExit && Input.GetKeyDown(KeyCode.Escape) && exitCooldown <= 0f)
        {
            StartCoroutine(FadeToWhiteAndLoadNextScene());
        }

        if (exitCooldown > 0f)
        {
            exitCooldown -= Time.deltaTime;
        }
    }

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

    IEnumerator PlayCutsceneWithFade()
    {
        hasStarted = true;
        pressFPlane.SetActive(false);

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

        endingPhotoImage.gameObject.SetActive(true);
    }

    IEnumerator FadeToWhiteAndLoadNextScene()
    {
        endingPhotoImage.gameObject.SetActive(false);

        yield return StartCoroutine(FadeColor(Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Stage1");  // 넘어갈 씬 이름으로 바꾸세요
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
