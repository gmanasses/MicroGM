using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    [Header("Audio Source")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SfxSource;

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
    public void PlayMusic(string clipName) {
        Sound musicToPlay = Array.Find(musicSounds, x => x.name == clipName);

        if(musicToPlay == null) {
            Debug.Log("ERROR: Music not found");
        
        } else {
            MusicSource.clip = musicToPlay.clip;
            MusicSource.Play();
        }
    }

    public void PlaySFX(string clipName) {
        Sound sfxToPlay = Array.Find(sfxSounds, x => x.name == clipName);

        if (sfxToPlay == null) {
            Debug.Log("ERROR: SFX not found");

        } else {
            SfxSource.PlayOneShot(sfxToPlay.clip);
        }
    }

}


[Serializable]
public class Sound {
    public string name;
    public AudioClip clip;
}