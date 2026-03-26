using UnityEngine;
using UnityEngine.UI;
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    public static OptionsMenu Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void OnEnable()
    {
        musicVolumeSlider.value = MusicPlayer.Instance.AudioSource.volume;
    }
    public void ChangleMusicVolume()
    {
        MusicPlayer.Instance.AudioSource.volume = musicVolumeSlider.value;
    }
}
