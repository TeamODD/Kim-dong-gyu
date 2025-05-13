using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    public Image startImage;           // ���� �Ϸ���Ʈ
    public Image fadeImage;           // ���̵�� �̹���
    public RawImage videoRawImage;    // ���� ��¿�
    public VideoPlayer videoPlayer;

    private bool hasStarted = false;
    private bool hasEnded = false;
    private bool canExit = false;

    void Start()
    {
        startImage.gameObject.SetActive(true);
        fadeImage.gameObject.SetActive(true);
        videoRawImage.enabled = false;
        fadeImage.color = new Color(0, 0, 0, 0); // ����
        videoPlayer.Stop();
    }

    void Update()
    {
        // �ƹ� Ű�� ����
        if (!hasStarted && Input.anyKeyDown)
        {
            hasStarted = true;
            StartCoroutine(PlayCutsceneWithFade());
        }

        // ESC 1: ���� ���� (��ŵ)
        if (hasStarted && !hasEnded && Input.GetKeyDown(KeyCode.Escape))
        {
            SkipCutscene();
        }

        // ���� ���� ����: ESC ��ٸ�
        if (hasStarted && !hasEnded && !videoPlayer.isPlaying && videoPlayer.time > 0.1f)
        {
            hasEnded = true;
            HoldLastFrame(); // ���̵� ���� ����
        }

        // ESC 2: ���� ��
        if (canExit && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(FadeToWhiteAndExit());
        }
    }

    void SkipCutscene()
    {
        videoPlayer.Pause(); // ����(������ ������ ����)
        hasEnded = true;
        HoldLastFrame();
    }

    void HoldLastFrame()
    {
        // ���� ������ ��� �״�� ����
        videoRawImage.enabled = true;
        canExit = true;
    }

    IEnumerator PlayCutsceneWithFade()
    {
        startImage.gameObject.SetActive(false);
        yield return StartCoroutine(Fade(0, 1, 1f));  // ���� ���̵� ��

        videoRawImage.enabled = true;
        videoPlayer.Play();
        yield return StartCoroutine(Fade(1, 0, 0.5f)); // ���� ���̵� �ƿ�
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
