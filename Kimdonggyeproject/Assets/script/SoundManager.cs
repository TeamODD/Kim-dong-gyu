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
            slider.onValueChanged.AddListener(SetBGMVolume); // ������
        }
    }


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // �� �̵��ص� ����
    }

    public void SetBGMVolume(float volume)
    {
        Debug.Log("�����̴� ���۵�, ����: " + volume);
        bgmSource.volume = volume;
    }
}
