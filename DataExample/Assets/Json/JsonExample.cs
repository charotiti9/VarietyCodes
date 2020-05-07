using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// 데이터 저장과 로드 함수
/// </summary>
public class JsonExample : MonoBehaviour
{
    [Tooltip("저장하길 원하는 파일 이름(.json 제외)")]
    public string fileName = "JsonSample";

    public enum DataType
    {
        Single,
        Array
    }
    [Tooltip("작성한 데이터의 타입 지정")]
    public DataType type = DataType.Single;

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

    public Data data;
    public Data[] arrayData;

    /// <summary>
    /// 데이터 save 및 load 경로
    /// </summary>
    string DataPath
    {
        get
        { return Path.Combine(Application.dataPath, "Json", fileName + ".json"); }
    }

    // test
    void Start()
    {
        Debug.Log(data.name);
        foreach (var data in arrayData)
        {
            Debug.Log(data.name);
        }
    }

    /// <summary>
    /// 데이터를 Json으로 저장
    /// </summary>
    [ContextMenu("Save Data To Json")]
    void SaveDataToJson()
    {
        string jsonData = "";

        if (type == DataType.Single)
            jsonData = JsonUtility.ToJson(data, true);
        else if (type == DataType.Array)
            jsonData = JsonArrayHelper.ToJson(arrayData, true);

        File.WriteAllText(DataPath, jsonData);
    }

    /// <summary>
    /// Json을 데이터로 불러오기
    /// </summary>
    [ContextMenu("Load Data From Json")]
    void LoadDataFromJson()
    {
        string jsonData = "";
        jsonData = File.ReadAllText(DataPath);

        if (type == DataType.Single)
            data = JsonUtility.FromJson<Data>(jsonData);
        else if (type == DataType.Array)
            arrayData = JsonArrayHelper.FromJson<Data>(jsonData);
    }
}

/// <summary>
/// Json을 Array로 사용하고 싶을 때 JsonUtility대신 사용
/// </summary>
public static class JsonArrayHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.array;
    }

    public static string ToJson<T>(T[] array, bool prettyPrint = false)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
