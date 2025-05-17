using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    public RawImage videoScreen;        // 영상 출력용 UI
    public VideoPlayer videoPlayer;     // VideoPlayer
    public Image fadeImage;             // 검은 화면용 UI Image (Canvas에 있어야 함)
    public GameObject pressFPlane; // Plane 오브젝트 연결할 변수
    public GameObject wasd;
    public GameObject esc;

    public string nextSceneName = "NextScene"; // 다음 씬 이름 입력
    private bool isPlayerNearby = false;
    private bool hasPlayed = false;
    private bool hasShownMessage = false;
    private bool hasStarted = false;
    private bool hasEnded = false;
    private bool canExit = false;
    private float exitCooldown = 0f; // ESC 연타 방지용


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 문 근처에 접근했습니다.");
            isPlayerNearby = true;

            if (!hasShownMessage)
            {
                pressFPlane.SetActive(true); // Plane을 보이게
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

            pressFPlane.SetActive(false); // 영역 벗어나면 다시 숨기기
        }
    }

    private void Start()
    {
        esc.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNearby && !hasPlayed && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("컷씬 진행");
            StartCoroutine(PlayCutsceneWithFade());
            wasd.SetActive(false);
        }

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
        wasd.SetActive(false);
        hasStarted = true;
        yield return StartCoroutine(Fade(0, 1, 1f));
        videoPlayer.Play();
        yield return StartCoroutine(Fade(1, 0, 0.5f));
    }

    void StopAndShowLastFrame()
    {
        esc.SetActive(true);
        videoScreen.enabled = true; // 꺼져 있었으면 다시 보여주기
        videoPlayer.frame = (long)videoPlayer.frameCount - 1;
        videoPlayer.Pause();
    }

    IEnumerator FadeToWhiteAndExit()
    {
        // 사진 숨기기
        if (esc != null)
            esc.gameObject.SetActive(false);

        yield return StartCoroutine(FadeColor(Color.clear, Color.white, 1f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Stage1-1");
    }
    IEnumerator Fade(float fromAlpha, float toAlpha, float duration)
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsed / duration);
            fadeImage.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(color.r, color.g, color.b, toAlpha);
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
