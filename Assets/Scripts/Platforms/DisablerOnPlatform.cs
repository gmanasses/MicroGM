using UnityEngine;
using UnityEngine.InputSystem;

public class DisablerOnPlatform : MonoBehaviour {

    // --- Private Declarations ---
    private PlayerController _playerController;
    private PlayerInput _pi;
    private int _initialJumpsAllowed;

    // --- Core Functions ---
    private void Start() {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _pi = PlayerController.FindObjectOfType(typeof(PlayerInput)) as PlayerInput;
        _initialJumpsAllowed = _playerController.GetJumpsAllowed();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") CancelJumps();
    }

    private void OnTriggerExit(Collider other) {        
        if (other.gameObject.tag == "Player") ResetJumps();
    }


    // --- Functions ---
    private void ResetJumps() {
        _playerController.SetJumpsAllowed(_initialJumpsAllowed);
    }

    public void CancelJumps() {
        _playerController.SetJumpsAllowed(-1);
    }

    public void DisablePlayerInput() {
        _pi.enabled = false;
        _playerController.enabled = false;
    }

    public void EnablePlayerInput() {
        _pi.enabled = true;
        _playerController.enabled = true;
    }

}
