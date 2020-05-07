using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{
    private string key = "Example";
    private int val = 0;
    public int defaultValue = 5;

    // 호출 시 갱신
    public int Val
    {
        get
        {
            LoadData();
            return val;
        }
        set
        {
            SaveData(value);
        }
    }

    // 시작 시에 로드
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey(key)) // 키가 있다면 로드
        {
            LoadData();
        }
        else // 키가 없다면 생성
        {
            Debug.Log("┌ Create new PlayerPrefs ┐");
            SaveData(defaultValue);
            Debug.Log("└───────────────┘");
        }
    }

    // 세이브
    private void SaveData(int data)
    {
        PlayerPrefs.SetInt(key, data);
        val = PlayerPrefs.GetInt(key, data);
        Debug.Log("Set: " + val);
    }
    // 로드
    private void LoadData()
    {
        val = PlayerPrefs.GetInt(key, val);
        Debug.Log("Get: " + val);
    }

}