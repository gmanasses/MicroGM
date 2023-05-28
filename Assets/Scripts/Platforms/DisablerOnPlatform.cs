using UnityEngine;
using UnityEngine.InputSystem;

public class DisablerOnPlatform : MonoBehaviour {

    // --- Private Declarations ---
    private PlayerController _playerController;
    private int _initialJumpsAllowed;

    // --- Core Functions ---
    private void Start() {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
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

}
