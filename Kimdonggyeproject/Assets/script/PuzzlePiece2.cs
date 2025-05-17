using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int correctOrder; // �� ������ �� ��° �������� (1, 2, 3, 4)

    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private Vector3 originalScale;
    public Vector3 selectedScale = new Vector3(1.2f, 1.2f, 1f); // �巡�� �� Ȯ��� ũ��


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
        transform.localScale = selectedScale; // �巡�� �� Ȯ��
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (isPlaced) return;

        transform.localScale = originalScale; // �׻� ���� ũ��� ����

        // Dropped on correct area? (Check in manager)
        PuzzleManager2.Instance.TryPlacePiece(this);
    }

    public void ResetPiece()
    {
        transform.SetParent(originalParent);
        transform.position = originalPosition;
        transform.localScale = originalScale; // ���� �� ũ�⵵ �������
        isPlaced = false;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
