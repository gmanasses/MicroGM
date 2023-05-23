using UnityEngine;

public class CheckpointController : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private Material[] _materials;
    private Renderer _renderer;
    private RespawnController _respawnController;
    private Collider _respawnCollider;


    // --- Core Functions ---
    private void Start() {
        _respawnController = GameObject.FindObjectOfType<RespawnController>();
        
        _renderer = GetComponent<Renderer>();
        _renderer.enabled = true;
        _renderer.sharedMaterial = _materials[0];

        _respawnCollider = GetComponent<Collider>();
        _respawnCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            ChangeCheckpointMaterial();
            UpdatePlayerRespawn();
            this.enabled = false;
            _respawnCollider.enabled = false;
        }
    }


    // --- Functions ---
    private void ChangeCheckpointMaterial() {
        _renderer.sharedMaterial = _materials[1];
    }

    private void UpdatePlayerRespawn() {
        _respawnController.UpdateRespawnPoint(this.transform);
    }

}
