using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;
    public AudioSource AudioSource;
    public int volume;
    private void Awake()
    {
        if (Instance != null && Instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        AudioSource = GetComponent<AudioSource>();
    }
}
