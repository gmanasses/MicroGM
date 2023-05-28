using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField, Range(2,5)] private int _maxHealth;
    private int _initialHealth;
    private int _currentHealth;

    private RespawnController _respawnController;
    private InterfaceController _interfaceController;


    // --- Core Functions ---
    private void Start() {
        _initialHealth = (int)(_maxHealth * 0.5);
        _currentHealth = _initialHealth;

        _respawnController = GetComponent<RespawnController>();

        _interfaceController = GameObject.FindObjectOfType<InterfaceController>();
        StartPlayerHearts();
    }


    // --- Functions ---
    public int GetCurrentHealth() {
        return this._currentHealth;
    }

    private void StartPlayerHearts() {
        for(int i=0; i<_initialHealth; i++) {
            _interfaceController.AddPlayerHeart(i);
        }
    }

    public void AddLife() {
        if(_currentHealth + 1 >= _maxHealth) {
            _currentHealth = _maxHealth;
        } else {
            _currentHealth += 1;
        }

        _interfaceController.AddPlayerHeart(_currentHealth-1);
    }

    public void RemoveLife() {
        _currentHealth -= 1;
        _interfaceController.RemovePlayerHeart(_currentHealth);

        if (_currentHealth < 1) {
            _respawnController.SetIfCantRespawn();
            _interfaceController.EnableOrDisableGameOverPanel(true);
        }
    }

}
