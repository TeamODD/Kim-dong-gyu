using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float targetSize = 10f;
    public float zoomDuration = 0.5f;

    private Coroutine zoomCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartZoom(5f); // Ȯ��
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartZoom(10f); // ���
        }
    }

    void StartZoom(float newSize)
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        zoomCoroutine = StartCoroutine(SmoothZoom(newSize));
    }

    System.Collections.IEnumerator SmoothZoom(float newSize)
    {
        float elapsed = 0f;
        float startSize = Camera.main.orthographicSize;

        while (elapsed < zoomDuration)
        {
            elapsed += Time.deltaTime;
            Camera.main.orthographicSize = Mathf.Lerp(startSize, newSize, elapsed / zoomDuration);
            yield return null;
        }

        Camera.main.orthographicSize = newSize;
    }
}
