using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("맞출 위치")]
    public Transform correctPos;
    [Header("스냅 허용 반경 (픽셀)")]
    public float snapThreshold = 50f;

    [HideInInspector]
    public bool isPlaced = false;

    [Header("클릭 시 커질 크기")]
    public Vector3 selectedScale = new Vector3(1.2f, 1.2f, 1f);  

    Vector3 originalPosition;
    Vector3 originalScale;
    Transform originalParent;
    CanvasGroup canvasGroup;

    void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
        originalScale = transform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        transform.position = eventData.position;
        transform.localScale = selectedScale;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        if (Vector3.Distance(transform.position, correctPos.position) <= snapThreshold)
        {
            // 스냅 & 고정
            transform.SetParent(correctPos);
            transform.localPosition = Vector3.zero;
            isPlaced = true;
            transform.localScale = originalScale; // 정답 위치에 놓으면 원래 크기로
            canvasGroup.blocksRaycasts = false;
            this.enabled = false;
        }
        else
        {
            transform.SetParent(originalParent);
            transform.position = originalPosition;
            transform.localScale = originalScale; // 틀린 위치에 놓아도 원래 크기로
        }

        PuzzleManager.Instance.CheckClear();
    }
}