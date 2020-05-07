using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableObjectExample : ScriptableObject
{
    /// <summary>
    /// 받고 싶은 데이터
    /// </summary>
    [System.Serializable]
    public class Data
    {
        public int id;
        public string name;
        public string[] texts;
        public Vector3 position;
    }


    public Data[] datas;
}
