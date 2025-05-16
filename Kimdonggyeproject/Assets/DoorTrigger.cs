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
    private bool isCutscenePlaying = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
            StartCoroutine(PlayCutscene());
            wasd.SetActive(false);
        }

        if (isCutscenePlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            // 즉시 컷씬 스킵 → 다음 씬으로 이동
            SkipToNextScene();
        }
    }

    IEnumerator PlayCutscene()
    {
        hasPlayed = true;

        // 페이드 인 (검은 화면 나타남)
        yield return StartCoroutine(Fade(0, 1, 1f));

        // 영상 켜기
        videoScreen.enabled = true;
        videoPlayer.Play();

        // 페이드 아웃 (영상 보여주기)
        yield return StartCoroutine(Fade(1, 0, 1f));

        // 영상 끝날 때까지 대기
        while (videoPlayer.isPlaying)
            yield return null;

        // 영상 끝났으면 페이드 인 → 화면 가리기
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            esc.SetActive(true);
            yield return StartCoroutine(Fade(0, 1, 1f));
        }

        // 영상/스크린 끄기
        videoPlayer.Stop();
        videoScreen.enabled = false;

        // 페이드 아웃 → 게임 재개
        yield return StartCoroutine(Fade(1, 0, 1f));
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

    void SkipToNextScene()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        StartCoroutine(SkipSceneCoroutine());
    }

    IEnumerator SkipSceneCoroutine()
    {
        // 영상 꺼지고 검정 화면 페이드 인 후 씬 이동
        yield return StartCoroutine(Fade(0, 1, 1f));
        LoadNextScene();
    }

    void LoadNextScene()
    {
        isCutscenePlaying = false;
        SceneManager.LoadScene(nextSceneName);
    }
}
