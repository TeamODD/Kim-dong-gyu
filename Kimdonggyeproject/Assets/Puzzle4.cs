using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Puzzle4 : MonoBehaviour
{

    private float[] angles = { 144f, -60f, 108f, -40f, -135f };
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

        if (_isSelectedDial == 0)
        {
            _currentDegree += 24f;
        }
        else if (_isSelectedDial == 1)
        {
            _currentDegree = 30f;
        }
        else if (_isSelectedDial == 2)
        {
            _currentDegree = 36f;
        }
        else if (_isSelectedDial == 3)
        {
            _currentDegree = 40f;
        }
        else if (_isSelectedDial == 4)
        {
            _currentDegree = 45f;
        }
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
                Debug.Log("Clear!");
                //모든 암호해결
            }
        }
    }

    void OnScrollDown()
    {
        Debug.Log("마우스 휠 ↓ 스크롤 감지!");
        // 아래로 스크롤 시 실행할 함수 내용
        if (_isSelectedDial == 0)
        {
            _currentDegree = -24f;
        }
        else if (_isSelectedDial == 1)
        {
            _currentDegree = -30f;
        }
        else if (_isSelectedDial == 2)
        {
            _currentDegree = -36f;
        }
        else if (_isSelectedDial == 3)
        {
            _currentDegree = -40f;
        }
        else if (_isSelectedDial == 4)
        {
            _currentDegree = -45f;
        }
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
                Debug.Log("Clear!");
                //모든 암호해결
            }
        }
    }
}