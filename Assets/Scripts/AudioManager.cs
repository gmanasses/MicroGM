using UnityEngine;

public class AudioManager : MonoBehaviour {

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip backgroundMenu;
    public AudioClip menuBar;


    // --- Core Functions ---
    private void Start() {
        musicSource.clip = backgroundMenu;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        sfxSource.PlayOneShot(clip);
    }

}
