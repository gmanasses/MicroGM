using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private GameObject _mainScreen;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _settingsScreen;
    [SerializeField] private GameObject _creditsScreen;
    [SerializeField] private GameObject _exitPopUp, _exitButton;


    // --- Core Functions ---
    private void Start() {
        _mainScreen.SetActive(true);
        _exitButton.SetActive(true);
        _loadingScreen.SetActive(false);
        _settingsScreen.SetActive(false);
        _creditsScreen.SetActive(false);
        _exitPopUp.SetActive(false);
    }


    // --- Functions ---
    public void ShowSettingsScreen() {
        _settingsScreen.SetActive(true);
        _mainScreen.SetActive(false);
        _exitButton.SetActive(false);
    }

    public void HideSettingsScreen() {
        _mainScreen.SetActive(true);
        _settingsScreen.SetActive(false);
        _exitButton.SetActive(true);
    }

    public void ShowCreditsScreen() {
        _creditsScreen.SetActive(true);
        _mainScreen.SetActive(false);
        _exitButton.SetActive(false);
    }

    public void HideCreditsScreen() {
        _mainScreen.SetActive(true);
        _creditsScreen.SetActive(false);
        _exitButton.SetActive(true);
    }

    public void ShowExitPopUp() {
        _mainScreen.SetActive(false);
        _exitButton.SetActive(false);
        _exitPopUp.SetActive(true);
    }

    public void HideExitPopUp() {
        _mainScreen.SetActive(true);
        _exitButton.SetActive(true);
        _exitPopUp.SetActive(false);
    }

    public void ShowLoadingScreen() {
        _loadingScreen.SetActive(true);
        _mainScreen.SetActive(false);
        _exitButton.SetActive(false);
    }

    public void LoadLevel(int SceneIndex) {
        StartCoroutine(LoadAsyncScene(SceneIndex));
    }

    private IEnumerator LoadAsyncScene(int SceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneIndex);

        ShowLoadingScreen();

        while (!operation.isDone) {
            yield return null;
        }
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
