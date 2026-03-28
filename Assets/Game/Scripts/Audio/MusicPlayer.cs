using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;
    public AudioSource AudioSource;
    public int volume;
    private void Awake()
    {
        if (Instance != null) Instance = this;
        AudioSource = GetComponent<AudioSource>();
    }
}
