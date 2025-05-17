using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class PuzzlePiece3 : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("정답 위치")]
    public Transform correctPos;

    [Header("정답 조각인지?")]
    public bool isCorrectPiece = false;

    [Header("스냅 허용 반경")]
    public float snapThreshold = 50f;

    [Header("클릭 시 커질 크기")]
    public Vector3 selectedScale = new Vector3(1.2f, 1.2f, 1f); // 드래그 중 커질 크기

    [HideInInspector]
    public bool isPlaced = false;

    Vector3 originalScale;
    Vector3 originalPosition;
    Transform originalParent;
    CanvasGroup canvasGroup;

    void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
        canvasGroup = GetComponent<CanvasGroup>();
        originalScale = transform.localScale; // 원래 크기 저장
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isPlaced) return;
        transform.position = eventData.position;
        transform.localScale = selectedScale; // 드래그 중 커짐
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isPlaced) return;

        transform.localScale = originalScale; // 항상 원래 크기로 복귀

        if (Vector3.Distance(transform.position, correctPos.position) <= snapThreshold)
        {
            transform.SetParent(correctPos);
            transform.localPosition = Vector3.zero;

            //  정답 조각일 경우
            if (isCorrectPiece)
            {
                isPlaced = true;
                canvasGroup.blocksRaycasts = false;
                this.enabled = false;
                PuzzleManager3.Instance.CheckClear(); // 즉시 클리어
            }
            else
            {
                //  틀린 조각 -> 리셋 연출
                PuzzleManager3.Instance.TriggerResetEffect();
            }
        }
        else
        {
            //  정답 칸에 닿지 않으면 원래 자리로
            ResetToOriginal();
        }
    }

    public void ResetToOriginal()
    {
        transform.SetParent(originalParent);
        transform.position = originalPosition;
        transform.localScale = originalScale; // 크기 원래대로
    }
}
