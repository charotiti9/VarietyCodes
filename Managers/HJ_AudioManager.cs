using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HJ_AudioManager : MonoBehaviour
{

    // 오디오들
    [HideInInspector]
    public AudioSource[] audios;

    // 클립들
    public AudioClip[] bgmClips;
    public AudioClip[] rainClips;
    public AudioClip[] uiClips;
    public AudioClip[] dieClips;

    // 일시정지 때 저장해 놓을 볼륨
    static float vol_0 = 0.5f;
    static float vol_1 = 0.5f;

    // 슬라이더
    static Slider[] volumeBar = new Slider[2];
    public Slider[] _volumeBar = new Slider[2];

    // 스스로 몇개 있는지 검사
    GameObject[] audioGameObject;

    #region 사운드 관리
    public enum BGSounds
    {
        BG_1,
        BG_2,
        BG_3
    }

    public enum UiSound
    {
        UiSound1,
        UiSound2,
        UiSound3,
        UiSound4,
        UiSound5
    }
    #endregion

    // 싱글톤
    public static HJ_AudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CheckDestroy();

        audios = GetComponents<AudioSource>();

        for (int i = 0; i < volumeBar.Length; i++)
        {
            volumeBar[i] = _volumeBar[i];
        }

        // 재시작 시에 볼륨바의 값 조정
        volumeBar[0].value = vol_0;
        volumeBar[1].value = vol_1;
    }

    /// <summary>
    /// 씬이 재로드 되어도 음악이 계속 이어지게 하기 위해 검사한다
    /// </summary>
    void CheckDestroy()
    {
        audioGameObject = GameObject.FindGameObjectsWithTag("AudioManager");

        if (audioGameObject.Length > 1)
        {
            Destroy(this.gameObject);
        }

        // 씬이 재로드 될 때, 이건 유지된다
        // 왜? 음악이 계속 이어지게 하기 위해서
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // 배경음과 빗소리 기본음향
        audios[0].volume = vol_0;
        audios[1].volume = vol_1;
    }

    /// <summary>
    /// BGM 관리는 0번째 오디오 소스가 한다
    /// </summary>
    /// <param name="bgm">재생할 BGM Clip을 Enum으로 가져온다</param>
    public void BGPlay(BGSounds bgm)
    {
        if (audios[0].clip != bgmClips[(int)bgm])
        {
            audios[0].clip = bgmClips[(int)bgm];
            audios[0].Play();
        }
    }

    /// <summary>
    /// UI 관리는 1번째 오디오 소스가 한다
    /// </summary>
    /// <param name="ui">재생할 UI Clip을 Enum으로 가져온다</param>
    public void UiPlay(UiSound ui)
    {
        audios[1].Stop();
        audios[1].clip = uiClips[(int)ui];
        audios[1].Play();
    }

    /// <summary>
    /// 오디오 소스를 멈춘다
    /// </summary>
    /// <param name="soundNum">멈출 오디오 소스</param>
    public void StopSounds(int soundNum)
    {
        audios[soundNum].Stop();
    }

    /// <summary>
    /// 음량조절 시 호출해주어야 볼륨이 조정된다.
    /// </summary>
    public void MusicVol()
    {
        audios[0].volume = volumeBar[0].value;
        audios[1].volume = volumeBar[1].value;
        vol_0 = volumeBar[0].value;
        vol_1 = volumeBar[1].value;
    }
}
