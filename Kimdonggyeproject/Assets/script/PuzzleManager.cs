using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    [Header("퍼즐 조각들")]
    public PuzzlePiece[] pieces;   // Hierarchy에서 4개의 PuzzlePiece 컴포넌트를 드래그해서 할당
    [Header("클리어 텍스트")]
    public TextMeshProUGUI clearText;         // Canvas 아래에 만든 Text UI, 인스펙터에 연결

    void Awake()
    {
        // 싱글턴 초기화
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // 클리어 메시지는 시작 시 비활성화
        clearText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 모든 퍼즐 조각이 제자리에 놓였는지 검사.
    /// </summary>
    public void CheckClear()
    {
        // 하나라도 false면 아직 클리어 아님
        foreach (var piece in pieces)
        {
            if (!piece.isPlaced)
                return;
        }

        // 모두 isPlaced == true
        ShowClear();
    }

    void ShowClear()
    {
        clearText.gameObject.SetActive(true);
        // 추가 효과(사운드, 애니메이션)가 필요하면 이곳에서 재생
    }
}