using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Sounds")]
    public Sound[] musicSounds;
    public Sound[] sfxSounds;


    // --- Core Functions ---
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        
        } else {
            Destroy(gameObject);
        }
    }

    public void Start() {
        PlayMusic("MenuBackground");
    }


    // --- Functions ---
    public void PlayMusic(string musicName) {
        Sound musicToPlay = Array.Find(musicSounds, x => x.name == musicName);

        if(musicToPlay == null) {
            Debug.LogError("ERROR: Music not found");
        
        } else {
            _musicSource.clip = musicToPlay.clip;
            _musicSource.Play();
        }
    }

    public void PlaySFX(string effectName) {
        Sound sfxToPlay = Array.Find(sfxSounds, x => x.name == effectName);

        if (sfxToPlay == null) {
            Debug.LogError("ERROR: SFX not found");

        } else {
            _sfxSource.PlayOneShot(sfxToPlay.clip);
        }
    }

    public void MusicVolume(float volume) {
        _musicSource.volume = volume;
    }

    public void SfxVolume(float volume) {
        _sfxSource.volume = volume;
    }

}


[Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
}