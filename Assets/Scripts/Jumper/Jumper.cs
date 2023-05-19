using UnityEngine;

public class Jumper : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private float _megaJumpStrength;
    private PlayerController _playerController;

    // --- Core Functions ---
    private void Start() {
        _playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            _playerController.MegaJump(_megaJumpStrength);
        }
    }

}
