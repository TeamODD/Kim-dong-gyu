using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutScene2 : MonoBehaviour
{
    public Image fadeImage;
    public RawImage videoRawImage;
    public VideoPlayer videoPlayer;
    public Image endingPhotoImage;

    private bool hasStarted = false;
    private bool hasEnded = false;
    private bool canExit = false;
    private float exitCooldown = 0f;

    public void PlayCutscene() 
    {
        if (!hasStarted)
        {
            hasStarted = true;
            StartCoroutine(PlayCutsceneWithFade());
        }
    }

    void Update()
    {
        if (!hasStarted) return;

        if (!hasEnded && Input.GetKeyDown(KeyCode.Escape) && exitCooldown <= 0f)
        {
            hasEnded = true;
            StopAndShowLastFrame();
            canExit = true;
            exitCooldown = 0.3f;
        }

        if (!hasEnded && !videoPlayer.isPlaying && videoPlayer.time > 0.1f)
        {
            hasEnded = true;
            StopAndShowLastFrame();
            canExit = true;
        }

        if (canExit && Input.GetKeyDown(KeyCode.Escape) && exitCooldown <= 0f)
        {
            StartCoroutine(FadeToWhiteAndExit());
        }

        if (exitCooldown > 0f)
            exitCooldown -= Time.deltaTime;
    }

    IEnumerator PlayCutsceneWithFade()
    {
        fadeImage.gameObject.SetActive(true);
        videoRawImage.enabled = true;
        videoPlayer.Stop();
        endingPhotoImage.gameObject.SetActive(false);

        yield return StartCoroutine(Fade(0, 1, 1f)); // Fade in to black

        videoPlayer.Play();
        yield return StartCoroutine(Fade(1, 0, 0.5f)); // Fade out to video
    }

    void StopAndShowLastFrame()
    {
        videoPlayer.frame = (long)videoPlayer.frameCount - 1;
        videoPlayer.Pause();
        videoRawImage.enabled = true;

        if (endingPhotoImage != null)
            endingPhotoImage.gameObject.SetActive(true);
    }

    IEnumerator FadeToWhiteAndExit()
    {
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
