using TMPro;
using UnityEngine;
using System.Collections;

public class PuzzleManager3 : MonoBehaviour
{
    public static PuzzleManager3 Instance { get; private set; }

    [Header("퍼즐 조각들")]
    public PuzzlePiece3[] pieces;
    [Header("클리어 텍스트")]
    public TextMeshProUGUI clearText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        clearText.gameObject.SetActive(false);
    }

    public void CheckClear()
    {
        // 정답 조각만 들어오면 클리어
        foreach (var piece in pieces)
        {
            if (piece.isCorrectPiece && piece.isPlaced)
            {
                ShowClear();
                return;
            }
        }
    }

    void ShowClear()
    {
        clearText.gameObject.SetActive(true);
        Debug.Log("Clear!");
    }

    public void TriggerResetEffect()
    {
        StartCoroutine(ShakeAndReset());
    }

    IEnumerator ShakeAndReset()
    {
        float shakeAmount = 10f;
        float duration = 0.3f;
        float timer = 0f;

        Vector3 origin = transform.position;

        while (timer < duration)
        {
            transform.position = origin + (Vector3)Random.insideUnitCircle * shakeAmount;
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = origin;

        //  틀린 퍼즐만 리셋 (정답 조각은 그대로)
        foreach (var piece in pieces)
        {
            if (!piece.isCorrectPiece)
                piece.ResetToOriginal();
        }
    }
}
