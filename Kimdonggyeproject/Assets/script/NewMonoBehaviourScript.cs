using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public Image startImage;           // 시작 일러스트
    public Image fadeImage;           // 검정색 페이드용 이미지
    public RawImage videoRawImage;    // 영상 출력용
    public VideoPlayer videoPlayer;

    private bool hasStarted = false;
    private bool hasEnded = false;

    void Start()
    {
        startImage.gameObject.SetActive(true);
        fadeImage.gameObject.SetActive(true);
        videoRawImage.enabled = false;
        fadeImage.color = new Color(0, 0, 0, 0); // 시작은 투명
        videoPlayer.Stop();
    }

    void Update()
    {
        if (!hasStarted && Input.anyKeyDown)
        {
            hasStarted = true;
            StartCoroutine(PlayCutsceneWithFade());
        }

        if (hasStarted && !hasEnded && !videoPlayer.isPlaying && videoPlayer.time > 0.1f)
        {
            hasEnded = true;
            //SceneManager.LoadScene("stage1");
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
}
