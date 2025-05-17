using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PuzzleManager2 : MonoBehaviour
{
    public static PuzzleManager2 Instance;

    public Transform[] correctSlots; // 4개의 정답 위치
    public PuzzlePiece2[] pieces;
    public TextMeshProUGUI clearText;

    private List<PuzzlePiece2> placedPieces = new List<PuzzlePiece2>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        clearText.gameObject.SetActive(false);
    }

    public void TryPlacePiece(PuzzlePiece2 piece)
    {
        foreach (Transform slot in correctSlots)
        {
            if (Vector3.Distance(piece.transform.position, slot.position) < 50f)
            {
                int index = placedPieces.Count;
                if (index < correctSlots.Length)
                {
                    piece.transform.SetParent(slot);
                    piece.transform.localPosition = Vector3.zero;
                    piece.isPlaced = true;
                    placedPieces.Add(piece);
                }

                // 모든 조각이 놓였을 때 자동으로 정답 체크
                if (placedPieces.Count == correctSlots.Length)
                {
                    CheckOrder();
                }
                return;
            }
        }

        piece.ResetPiece();
    }

    void CheckOrder()
    {
        for (int i = 0; i < placedPieces.Count; i++)
        {
            if (placedPieces[i].correctOrder != i + 1)
            {
                StartCoroutine(ShakeAndReset());
                return;
            }
        }

        // 순서 맞음 → 클리어!
        clearText.gameObject.SetActive(true);
        clearText.text = "Clear!";
    }


    IEnumerator ShakeAndReset()
    {
        float duration = 0.3f;
        float magnitude = 10f;
        Vector3 originalPos = transform.position;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = originalPos + Random.insideUnitSphere * magnitude;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;

        // 리셋
        foreach (var piece in pieces)
        {
            piece.ResetPiece();
        }
        placedPieces.Clear();
    }
}
