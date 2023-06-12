using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpeechManager: MonoBehaviour {

    // --- Public Declarations ---
    public static bool SpeechIsActive = false;

    // --- Private Declarations ---
    [Header("Speech Content")]
    [SerializeField] private Image _characterIcon; 
    [SerializeField] private Text _characterName;
    [SerializeField] private Text _speechContent;
    private InterfaceController _interfaceController;
    private MessageContent[] _messagesToShow;
    private Character[] _charactersToShow;
    private int _currentMessageIndex = 0;

    [Header("Writer")]
    [SerializeField, Range(0, 0.1f)] private float _timeBetweenChars;
    private IEnumerator _stringScribe;


    // --- Core Fuctions ---
    private void Start() {
        _interfaceController = GameObject.FindObjectOfType<InterfaceController>();
    }

    private void Update() {
        if(SpeechIsActive) {
            if (Input.GetKeyDown(KeyCode.E)) {
                if (_stringScribe != null) {
                    StopCoroutine(_stringScribe);
                }

                NextMessage();
            }
        }
    }


    // --- Fuctions ---
    public void OpenSpeech(MessageContent[] _messages, Character[] _characters) {
        SpeechIsActive = true;

        _messagesToShow = _messages;
        _charactersToShow = _characters;
        _currentMessageIndex = 0;

        ShowMessage();
        _interfaceController.EnableOrDisableSpeechObj(true);
    }

    private void NextMessage() {
        _currentMessageIndex++;
        if(_currentMessageIndex < _messagesToShow.Length) {
            ShowMessage();

        } else {
            SpeechIsActive = false;
            _interfaceController.EnableOrDisableSpeechObj(false);
            _interfaceController.FadeInFinish();
        }
    }

    private void ShowMessage() {
        MessageContent messageToShow = _messagesToShow[_currentMessageIndex];
        _speechContent.text = "";
        _stringScribe = WritePhraseGradually(messageToShow.message, _timeBetweenChars);
        StartCoroutine(_stringScribe);

        Character characterToShow = _charactersToShow[messageToShow._characterID];
        _characterName.text = characterToShow.name;
        _characterIcon.sprite = characterToShow.faceImage;
    }

    private IEnumerator WritePhraseGradually(string phrase, float timeBetweenChars) {
        foreach (char letter in phrase) {
            _speechContent.text += letter;
            yield return new WaitForSecondsRealtime(timeBetweenChars);
        }
    }
    
}
