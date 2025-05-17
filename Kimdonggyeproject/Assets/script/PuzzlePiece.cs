using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class PuzzlePiece : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("���� ��ġ")]
    public Transform correctPos;
    [Header("���� ��� �ݰ� (�ȼ�)")]
    public float snapThreshold = 50f;

    [HideInInspector]
    public bool isPlaced = false;

    [Header("Ŭ�� �� Ŀ�� ũ��")]
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
            // ���� & ����
            transform.SetParent(correctPos);
            transform.localPosition = Vector3.zero;
            isPlaced = true;
            transform.localScale = originalScale; // ���� ��ġ�� ������ ���� ũ���
            canvasGroup.blocksRaycasts = false;
            this.enabled = false;
        }
        else
        {
            transform.SetParent(originalParent);
            transform.position = originalPosition;
            transform.localScale = originalScale; // Ʋ�� ��ġ�� ���Ƶ� ���� ũ���
        }

        PuzzleManager.Instance.CheckClear();
    }
}