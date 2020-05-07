using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectTester : MonoBehaviour
{
    [SerializeField]
    private ScriptableObjectExample example;

    private void Start()
    {
        foreach (var data in example.datas)
        {
            Debug.Log(data.name);
        }
    }
}
