using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HJ_ScoreManager : MonoBehaviour
{
    // 스코어
    int score;
    // 탑스코어
    [HideInInspector]
    public int topScore;

    // 현재점수 GUI
    public Text[] curScoreText;
    // 최고점수 GUI
    public Text[] topScoreText;

    // 싱글톤
    public static HJ_ScoreManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // 최고점수 로드해온다
        if (PlayerPrefs.HasKey("TopScore"))
        {
            topScore = PlayerPrefs.GetInt("TopScore", topScore);
        }
        else
        {
            PlayerPrefs.SetInt("TopScore", 0);
            topScore = PlayerPrefs.GetInt("TopScore", topScore);
        }

        // 초기 UI
        ScoreUiInit();
    }

    /// <summary>
    /// 스코어 Get/Set Property
    /// </summary>
    public int Score
    {
        // 스코어를 가져올 때 사용
        get
        {
            return score;
        }
        // 스코어를 수정할 때 사용
        set
        {
            // 스코어 값은 외부에서 설정해줌
            score = value;
            // 스코어 값을 따라서 text 갱신
            ScoreUiRenew();
        }
    }

    /// <summary>
    /// GUI를 표시해주고 싶을 때 사용
    /// </summary>
    public void ScoreUiInit()
    {
        // GUI 표시
        // 현재점수 0으로 초기화
        for (int i = 0; i < curScoreText.Length; i++)
        {
            curScoreText[i].text = "" + 0;
        }
        // 최고점수 로드한 숫자로 설정
        for (int i = 0; i < topScoreText.Length; i++)
        {
            topScoreText[i].text = "" + topScore;
        }
    }

    /// <summary>
    /// 스코어 값을 따라서 text 갱신
    /// </summary>
    public void ScoreUiRenew()
    {
        for (int i = 0; i < curScoreText.Length; i++)
        {
            curScoreText[i].text = "" + score;
        }
    }
}
