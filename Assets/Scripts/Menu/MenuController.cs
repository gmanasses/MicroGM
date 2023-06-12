using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private GameObject _loadingScreen, _menuBarsTriggers;


    // --- Core Functions ---
    private void Start() {
        _menuBarsTriggers.SetActive(true);
        _loadingScreen.SetActive(false);
    }


    // --- Functions ---
    public void ShowLoadingScreen() {
        _menuBarsTriggers.SetActive(false);
        _loadingScreen.SetActive(true);
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
