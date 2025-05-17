using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;


public class EndManager : MonoBehaviour
{
    public GameObject EndImage;
    public GameObject ESC;
    public RawImage videoRawImage;
    public Image fadeImage;
    public VideoPlayer videoPlayer;

    private bool videoend = false;
    private bool canExit = false;
    private bool quit = false;

    private float exitCooldown = 0f; // ESC 연타 방지용

    void Start()
    {
        ESC.SetActive(false);
        EndImage.SetActive(false);
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0);
        videoRawImage.enabled = true;

        Fade(0, 1, 2f);
        videoPlayer.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            videoend = true;
            videoPlayer.Stop();
            StopAndShowLastFrame();
            exitCooldown = 0.3f;
        }

        if(!videoPlayer.isPlaying&& videoPlayer.time > 30f)
        {
            videoend = true;
            StopAndShowLastFrame();
        }

        if (canExit && Input.GetKeyDown(KeyCode.Escape) && exitCooldown <= 0f)
        {
            Fade(1, 0, 2f);
            EndImage.SetActive(true);
            quit = true;
        }

        if (quit && Input.anyKeyDown)
        {
            Application.Quit();
        }
    }

    void StopAndShowLastFrame()
    {
        videoPlayer.frame = (long)videoPlayer.frameCount - 1;
        videoPlayer.Pause();
        canExit = true;
        videoRawImage.enabled = true;
        ESC.SetActive(true);
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
