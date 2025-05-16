using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int correctOrder; // 이 조각이 몇 번째 조각인지 (1, 2, 3, 4)

    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private Vector3 originalScale;
    public Vector3 selectedScale = new Vector3(1.2f, 1.2f, 1f); // 드래그 중 확대될 크기


    [HideInInspector] public bool isPlaced = false;

    void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
        originalScale = transform.localScale;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        transform.position = eventData.position;
        transform.localScale = selectedScale; // 드래그 중 확대
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (isPlaced) return;

        transform.localScale = originalScale; // 항상 원래 크기로 복귀

        // Dropped on correct area? (Check in manager)
        PuzzleManager2.Instance.TryPlacePiece(this);
    }

    public void ResetPiece()
    {
        transform.SetParent(originalParent);
        transform.position = originalPosition;
        transform.localScale = originalScale; // 리셋 시 크기도 원래대로
        isPlaced = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
