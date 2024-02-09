using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour {

    // --- Private Declarations ---
    private Canvas _canvas;
    private float _halfCanvasHeight;

    [Header("Tutorial")]
    [SerializeField] private Image _tutorialBackground;
    [SerializeField] private GameObject _tutorialBanner;
    [SerializeField] private GameObject _tutorialJump;
    [SerializeField] private GameObject _tutorialMovement;
    [SerializeField, Range(0, 1)] private float _timeBetweenTutorialParts;
    [SerializeField, Range(0, 5)] private float _timeShowingTutorialPart;
    [SerializeField, Range(0, 3)] private float _animationTimeTutorial;
    [SerializeField] private UnityEvent _whenTutorialsClose;

    [Header("Activation Button")]
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField, Range(0, 1)] private float _animationTimeInteractionPanel;

    [Header("Player Health")]
    [SerializeField] private GameObject[] _playerHearts;
    [SerializeField, Range(0, 2)] private float _animationTimePlayerHeart;

    [Header("Speech System")]
    [SerializeField] private GameObject _speech;
    [SerializeField, Range(0, 2)] private float _animationTimeSpeech;

    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private RectTransform _restartText;
    [SerializeField, Range(0, 2)] private float _animationTimeGameOver;
    [SerializeField, Range(0, 4)] private float _animationTimeGameOverRestartPhrase;
    private float _halfPanelHeight;

    [Header("Finish Game")]
    [SerializeField] private Image _finishBackground;
    [SerializeField] private GameObject _finishText;
    [SerializeField, Range(0, 3)] private float _animationTimeFinish;


    // --- Core Fuctions ---
    private void Start() {
        _canvas = GetComponent<Canvas>();
        _halfCanvasHeight = _canvas.GetComponent<RectTransform>().rect.height / 2;
        _halfPanelHeight = _gameOverPanel.GetComponent<RectTransform>().rect.height / 2;

        if(PlayerPrefs.HasKey("hasSeenTutorial")) {
            DisableTutorial();

        } else {
            StartCoroutine(ShowTutorial());
        }
    }


    // --- Fuctions ---
    public void DisableTutorial() {
        _tutorialBackground.gameObject.SetActive(false);
        OnCloseTutorial();
    }

    private IEnumerator ShowTutorial() {
        //tutorial banner with text
        LeanTween.scale(_tutorialBanner, Vector3.one, _animationTimeTutorial).setDelay(.2f).setEase(LeanTweenType.easeOutElastic);
        yield return new WaitForSecondsRealtime(_timeShowingTutorialPart);
        LeanTween.scale(_tutorialBanner, Vector3.zero, _animationTimeTutorial).setDelay(.2f).setEase(LeanTweenType.easeInOutElastic);
        yield return new WaitForSecondsRealtime(_timeBetweenTutorialParts);

        //show jump controller
        LeanTween.scale(_tutorialJump, Vector3.one, _animationTimeTutorial).setDelay(.2f).setEase(LeanTweenType.easeOutElastic);
        yield return new WaitForSecondsRealtime(_timeShowingTutorialPart);
        LeanTween.scale(_tutorialJump, Vector3.zero, _animationTimeTutorial).setDelay(.2f).setEase(LeanTweenType.easeInOutElastic);
        yield return new WaitForSecondsRealtime(_timeBetweenTutorialParts);

        //show movement controller
        LeanTween.scale(_tutorialMovement, Vector3.one, _animationTimeTutorial).setDelay(.2f).setEase(LeanTweenType.easeOutElastic);
        yield return new WaitForSecondsRealtime(_timeShowingTutorialPart);
        LeanTween.scale(_tutorialMovement, Vector3.zero, _animationTimeTutorial).setDelay(.2f).setEase(LeanTweenType.easeInOutElastic).setOnComplete(FinishTutorial);
    }

    private void FinishTutorial() {
        _tutorialBackground.transform.parent.gameObject.SetActive(false);
        OnCloseTutorial();
    }

    private void OnCloseTutorial() {
        _whenTutorialsClose.Invoke();
    }

    public void EnableOrDisableInteractionPanel(bool enable) {
        if(enable) {
            LeanTween.scale(_interactionPanel, Vector3.one, _animationTimeInteractionPanel).setEase(LeanTweenType.easeOutBack);
        
        } else {
            LeanTween.scale(_interactionPanel, Vector3.zero, _animationTimeInteractionPanel).setEase(LeanTweenType.easeInBack);
        }      
    }

    public void AddPlayerHeart(int index) {
        LeanTween.scale(_playerHearts[index], Vector3.one, _animationTimePlayerHeart).setDelay(.3f).setEase(LeanTweenType.easeOutElastic);
    }

    public void RemovePlayerHeart(int index) {
        LeanTween.scale(_playerHearts[index], Vector3.zero, _animationTimePlayerHeart).setDelay(.3f).setEase(LeanTweenType.easeInOutElastic);
    }

    public void EnableOrDisableSpeechObj(bool enable) {
        if(enable) {
            LeanTween.scale(_speech, Vector3.one, _animationTimeSpeech).setEase(LeanTweenType.easeOutExpo);

        } else {
            LeanTween.scale(_speech, Vector3.zero, _animationTimeSpeech).setEase(LeanTweenType.easeInExpo);
        }
    }

    public void EnableOrDisableGameOverPanel(bool enable) {
        if(enable) {
            LeanTween.moveY(_gameOverPanel, _halfCanvasHeight - _halfPanelHeight/2, _animationTimeGameOver).setEase(LeanTweenType.easeOutQuint);
            LeanTween.alphaText(_restartText, 1, _animationTimeGameOverRestartPhrase).setEase(LeanTweenType.easeInOutSine).setLoopPingPong();

        } else {
            LeanTween.moveY(_gameOverPanel, 0 - _halfPanelHeight, _animationTimeGameOver).setEase(LeanTweenType.easeInQuint);
        }
    }

    public void FadeInFinish() {
        LeanTween.alpha(_finishBackground.rectTransform, 1, _animationTimeFinish).setEase(LeanTweenType.easeInCirc).setOnComplete(FadeFinishText);
    }

    private void FadeFinishText() {
        LeanTween.scale(_finishText, Vector3.one, _animationTimeFinish).setEase(LeanTweenType.easeOutCirc);
    }

}
