using System.Collections.Generic;
using UnityEngine;

public class ObjectActivationManage : MonoBehaviour
{
    public List<GameObject> ObjectActive;
    public static int ActiveIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < ObjectActive.Count; i++)
        {
            //ObjectActive[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectActivationManage.ActiveIndex != 0)
        {
            ObjectActive[ActiveIndex].SetActive(true);
        }
    }
}
