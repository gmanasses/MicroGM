using UnityEngine;

public class LevelManager : MonoBehaviour {

    // --- Private Declarations ---
    public static LevelManager Instance;
    [SerializeField] private bool _alreadySeenFirstTutorial;


    // --- Core Functions ---
    private void Awake() {
        if(Instance != null) {
            Destroy(this.gameObject);

        } else {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    // --- Functions ---
    public bool GetFirstTutorialStatus() {
        return _alreadySeenFirstTutorial;
    }

    public void SetFirstTutorialStatus() {
        this._alreadySeenFirstTutorial = true;
    }

}
