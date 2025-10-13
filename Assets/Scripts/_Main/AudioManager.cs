using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private List<Audio> _audios;

    private Audio _currentThemeAudio;

    private Dictionary<string, Audio> _audioDict = new Dictionary<string, Audio>();

    private void OnLevelWasLoaded(int level)
    {
        SetThemeAudio(SceneManager.GetActiveScene().name);
        StopSound("Footstep");
    }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(Instance);
        
        DontDestroyOnLoad(Instance);
        
        Initialized();
    }

    public static void PlaySound(string soundName)
    {
        try
        {
            Instance._audioDict[soundName].Play();
        }
        catch
        {
            Debug.LogWarning("Sound don't found");
        }
    }

    public static void StopSound(string soundName)
    {
        try
        {
            Instance._audioDict[soundName].Stop();
        }
        catch
        {
            Debug.LogWarning("Sound don't found");
        }
    }

    private void Initialized()
    {
        foreach (Audio audio in _audios)
        {
            AudioSource source = transform.AddComponent<AudioSource>();
            source.volume = audio.Volume;
            source.loop = audio.IsLoop;
            source.pitch = audio.Pitch;

            audio.SetSource(source);

            _audioDict.Add(audio.Name, audio);
        }

        SetThemeAudio(SceneManager.GetActiveScene().name);
    }

    private void SetThemeAudio(string sceneName)
    {
        if (_currentThemeAudio != null) _currentThemeAudio.Stop();

        _currentThemeAudio = _audioDict[sceneName];
        _currentThemeAudio.Play();
    }
}

[System.Serializable]
public class Audio
{
    [field: SerializeField] public string Name { get; private set; }
    [field: Range(0, 1)]
    [field: SerializeField] public float Volume { get; private set; }
    [field: SerializeField] public bool IsLoop { get; private set; }
    [field: Range(-3, 3)]
    [field: SerializeField] public float Pitch { get; private set; }
    [field: SerializeField] public AudioClip[] Clips { get; private set; }
    [field: SerializeField] public AudioSource Source { get; private set; }

    public void SetSource(AudioSource source) => Source = source;

    public void Play()
    {
        Source.clip = GetRandomClip();

        if (Source.isPlaying == true && IsLoop == true) return;

        Source.Play();
    }

    public void Stop() => Source.Stop();

    private AudioClip GetRandomClip()
    {
        int radomValue = Random.Range(0, Clips.Length);
        return Clips[radomValue];
    }
}
