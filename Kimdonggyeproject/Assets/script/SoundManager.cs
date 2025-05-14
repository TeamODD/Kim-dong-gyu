using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource bgmSource;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        bgmSource.volume = savedVolume;

        Slider slider = GameObject.Find("BGMVolumeSlider").GetComponent<Slider>();
        if (slider != null)
        {
            slider.value = savedVolume;
            slider.onValueChanged.AddListener(SetBGMVolume); // 안전망
        }
    }


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // 씬 이동해도 유지
    }

    public void SetBGMVolume(float volume)
    {
        Debug.Log("슬라이더 조작됨, 볼륨: " + volume);
        bgmSource.volume = volume;
    }
}
