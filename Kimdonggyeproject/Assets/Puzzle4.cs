using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Puzzle4 : MonoBehaviour
{
    
    private float[] angles = new float[] { 0f, 45f, 90f, 135f, 180f }; // 0 <= theta <= 360 , 사진각도
    private float _currentDegree;
    private int currentSelectedIndex;
    public List<GameObject> DialGameObjectList = new List<GameObject>();
    private bool[] _isSolved = new bool[5];
    private int _isSelectedDial = 0;
    public bool isPlayingPuzzle4;


    void Start()
    {
        _currentDegree = 0;
    }
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(_isSelectedDial + "이 선택되었습니다.");
            _currentDegree = 0;
            if (_isSelectedDial > 0 && _isSelectedDial < DialGameObjectList.Count)
            {
                _isSelectedDial -= 1;
            }
            /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //충돌
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 100f);
            RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
            for (int i = 0; i < hits.Length; i++)
            { 
                RaycastHit hit = hits[i];
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("=== Raycast Hit Info ===");
                    Debug.Log($"GameObject Name: {hit.collider.gameObject.name}");
                    GameObject clickedObject = hit.collider.gameObject;

                    int idx = DialGameObjectList.IndexOf(clickedObject);

                    if (idx != -1)
                    {
                        Debug.Log($"{clickedObject.name}이 indexList 안에 있음! 인덱스: {idx}");
                        _isSelectedDial = true;
                        // Z 변경?
                        currentSelectedIndex = idx;
                    }
                    else
                    {
                        _isSelectedDial = false;
                    }
                }
            }*/
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(_isSelectedDial + "이 선택되었습니다.");
            _currentDegree = 0;
            if (_isSelectedDial >= 0 && _isSelectedDial < DialGameObjectList.Count - 1)
            {
                _isSelectedDial += 1;
            }
        }
        if (scroll > 0f)
        {
            OnScrollUp();
        }
        else if (scroll < 0f)
        {
            OnScrollDown();
        }
    }

    private bool _isSolvedDial()
    {
        bool isFinished = true;
        for (int i = 0; i <= 4; i++)
        {
            if (_isSolved[i] == false)
            {
                isFinished = false;
            }
        }
        return isFinished;
    }
    void OnScrollUp()
    {
        Debug.Log("마우스 휠 ↑ 스크롤 감지!");
        // 위로 스크롤 시 실행할 함수 내용

            _currentDegree += 15f;
            if (_currentDegree >= 360)
            {
                _currentDegree = _currentDegree - 360;
            }
            DialGameObjectList[_isSelectedDial].transform.Rotate(0, 0, _currentDegree);
            if (_currentDegree == angles[_isSelectedDial])
            {
                _isSolved[_isSelectedDial] = true;
                if (_isSolvedDial())
                {
                    //모든 암호해결
                }
            }
    }

    void OnScrollDown()
    {
        Debug.Log("마우스 휠 ↓ 스크롤 감지!");
        // 아래로 스크롤 시 실행할 함수 내용
            _currentDegree -= 15f;
            if (_currentDegree < 0)
            {
                _currentDegree = _currentDegree + 360;
            }
            DialGameObjectList[_isSelectedDial].transform.Rotate(0, 0, _currentDegree);
            if (_currentDegree == angles[_isSelectedDial])
            {
                _isSolved[_isSelectedDial] = true;
                if (_isSolvedDial())
                {
                    //모든 암호해결
                }
            }
    }
}
