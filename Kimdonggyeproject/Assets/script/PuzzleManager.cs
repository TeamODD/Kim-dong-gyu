using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    [Header("���� ������")]
    public PuzzlePiece[] pieces;   // Hierarchy���� 4���� PuzzlePiece ������Ʈ�� �巡���ؼ� �Ҵ�
    [Header("Ŭ���� �ؽ�Ʈ")]
    public TextMeshProUGUI clearText;         // Canvas �Ʒ��� ���� Text UI, �ν����Ϳ� ����

    void Awake()
    {
        // �̱��� �ʱ�ȭ
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Ŭ���� �޽����� ���� �� ��Ȱ��ȭ
        clearText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ��� ���� ������ ���ڸ��� �������� �˻�.
    /// </summary>
    public void CheckClear()
    {
        // �ϳ��� false�� ���� Ŭ���� �ƴ�
        foreach (var piece in pieces)
        {
            if (!piece.isPlaced)
                return;
        }

        // ��� isPlaced == true
        ShowClear();
    }

    void ShowClear()
    {
        clearText.gameObject.SetActive(true);
        // �߰� ȿ��(����, �ִϸ��̼�)�� �ʿ��ϸ� �̰����� ���
    }
}