using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : MonoBehaviour
{
    
    private float[] angles = new float[] { 0f, 45f, 90f, 135f, 180f }; // -180 <= theta <= 180 , ��������
    private float _currentDegree;
    private int currentSelectedIndex;
    public List<GameObject> DialGameObjectList = new List<GameObject>();
    private bool[] _isSolved = new bool[5];
    private bool _isSelectedDial;


    void Start()
    {
        _currentDegree = 0;
    }
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Input.GetMouseButtonDown(0))
        {
            _currentDegree = 0;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //�浹
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
                        Debug.Log($"{clickedObject.name}�� indexList �ȿ� ����! �ε���: {idx}");
                        _isSelectedDial = true;
                        currentSelectedIndex = idx;
                    }
                    else
                    {
                        _isSelectedDial = false;
                    }
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
        Debug.Log("���콺 �� �� ��ũ�� ����!");
        // ���� ��ũ�� �� ������ �Լ� ����
        
        if (_isSelectedDial == true)
        {
            _currentDegree += 15f;
            if (_currentDegree >= 360)
            {
                _currentDegree = _currentDegree - 360;
            }
            DialGameObjectList[currentSelectedIndex].transform.Rotate(0, _currentDegree, 0);
            if (_currentDegree == angles[currentSelectedIndex])
            {
                _isSolved[currentSelectedIndex] = true;
                if (_isSolvedDial())
                {
                    //��� ��ȣ�ذ�
                }
            }
        }
    }

    void OnScrollDown()
    {
        Debug.Log("���콺 �� �� ��ũ�� ����!");
        // �Ʒ��� ��ũ�� �� ������ �Լ� ����
        if (_isSelectedDial == true)
        {
            _currentDegree -= 15f;
            if (_currentDegree < 0)
            {
                _currentDegree = _currentDegree + 360;
            }
            DialGameObjectList[currentSelectedIndex].transform.Rotate(0, _currentDegree, 0);
            if (_currentDegree == angles[currentSelectedIndex])
            {
                _isSolved[currentSelectedIndex] = true;
                if (_isSolvedDial())
                {
                    //��� ��ȣ�ذ�
                }
            }
        }
    }
}
