using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField, Range(-15, -4)] private float _heightToRespawn;
    [SerializeField] private Transform _spawnPoint;
    private Transform _mainRespawnPoint;
    private bool _wantRespawn, _canRespawn, _inGameOverStage;

    private PlayerStatus _playerStatus;


    // --- Core Functions ---
    private void Start() {
        _mainRespawnPoint = _spawnPoint;
        _canRespawn = true; 
        _wantRespawn = false;

        _playerStatus = GetComponent<PlayerStatus>();
    }

    private void Update() {
        if(_inGameOverStage) {
            if(Input.anyKey) {
                FindObjectOfType<LevelManager>().SetFirstTutorialStatus();
                SceneManager.LoadScene("Master");
            }
        }
    }

    private void FixedUpdate() {
        if(this.transform.position.y < _heightToRespawn) SetIfWantRespawn();

        if(_wantRespawn && _canRespawn) {
            DoRespawn();
            _playerStatus.RemoveLife();
            _wantRespawn = false;
        }
    }


    // --- Functions ---
    public void SetIfWantRespawn() {
        this._wantRespawn = true;
    }

    public void SetIfCantRespawn() {
        this._canRespawn = false;
    }

    public void SetGameOverStage(bool isInGameOver) {
        _inGameOverStage = isInGameOver;
    }

    public void UpdateRespawnPoint(Transform checkpointTransform) {
        this._spawnPoint = checkpointTransform;
    }

    public void DoRespawn() {
        this.transform.position = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y, _spawnPoint.position.z);
    }

    public void SetRespawnToMain() {
        this._spawnPoint = _mainRespawnPoint;
    }

}
