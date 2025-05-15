using System.Collections.Generic;
using UnityEngine;

public class ObjectActivationManage : MonoBehaviour
{
    public GameObject PuzzleBackground;
    public List<GameObject> ObjectActive;
    public static int currentPuzzleIndex = 0;
    public static bool isPlayingPuzzle = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 1; i < ObjectActive.Count; i++)
        {
            ObjectActive[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ObjectActivation();
        ShowPuzzleBackground();
    }

    public void ShowPuzzleBackground()
    {
        if (isPlayingPuzzle)
        {
            PuzzleBackground.SetActive(true);
        }
        else
        {
            PuzzleBackground.SetActive(false);
        }
    }

    public void ObjectActivation()
    {
        if (currentPuzzleIndex > 0)
        {
            ObjectActive[currentPuzzleIndex].SetActive(true);
        }
    }
}
