using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour {

    [SerializeField] private AudioMixer _gameAudioMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;


    // --- Core Functions ---
    private void Start() {
        if(PlayerPrefs.HasKey("MusicVolumeValue") && PlayerPrefs.HasKey("SfxVolumeValue")) {
            LoadSavedVolume();
        
        } else {
            SetMusicVolume();
            SetSfxVolume();
        }
    }


    // --- Functions ---
    public void SetMusicVolume() {
        float newVolume = _musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolumeValue", newVolume);

        newVolume = Mathf.Log10(newVolume) * 20;
        _gameAudioMixer.SetFloat("MusicVolumeParam", newVolume);
    }

    public void SetSfxVolume() {
        float newVolume = _sfxSlider.value;
        PlayerPrefs.SetFloat("SfxVolumeValue", newVolume);

        newVolume = Mathf.Log10(newVolume) * 20;
        _gameAudioMixer.SetFloat("SfxVolumeParam", newVolume);
    }

    public void LoadSavedVolume() {
        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolumeValue");
        SetMusicVolume();

        _sfxSlider.value = PlayerPrefs.GetFloat("SfxVolumeValue");
        SetSfxVolume();
    }

}
