using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> images;
    public GameObject clearText;

    private readonly float[] goals = { 144f, 300f, 108f, 320f, 225f }; //144, -60, 108, -40, -135

    private int currentIndex = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            currentIndex = (currentIndex + 1) % images.Count;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (currentIndex == 0)
            {
                images[currentIndex].transform.Rotate(0, 0, 24f);
            }
            if (currentIndex == 1)
            {
                images[currentIndex].transform.Rotate(0, 0, 30f);
            }
            if (currentIndex == 2)
            {
                images[currentIndex].transform.Rotate(0, 0, 36f);
            }
            if (currentIndex == 3)
            {
                images[currentIndex].transform.Rotate(0, 0, 40f);
            }
            if (currentIndex == 4)
            {
                images[currentIndex].transform.Rotate(0, 0, 45f);
            }
        }

        int correctCount = 0;

        for (int i = 0; i < images.Count; i++)
        {
            float zAngle = images[i].transform.rotation.eulerAngles.z;
            if (Mathf.Abs(Mathf.DeltaAngle(zAngle, goals[i])) < 0.3f)
            {
                correctCount++;
            }
        }
        //correctCount == images.Count라면
        //3초간 텍스트 띄우고 슬립
        //클리어 하면 퍼즐 다시 못하게
        //
        clearText.SetActive(correctCount == images.Count);
    }
}