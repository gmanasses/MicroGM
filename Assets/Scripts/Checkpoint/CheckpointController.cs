using UnityEngine;
using UnityEngine.Events;

public class CheckpointController : MonoBehaviour {

    // --- Private Declarations ---
    [Header("Checkpoint")]
    [SerializeField] private Material[] _materials;
    private Renderer _checkpointRenderer;
    private Collider _checkpointCollider;
    private RespawnController _respawnController;

    [Header("Events")]
    [SerializeField] private UnityEvent _whenPlayerPass;


    // --- Core Functions ---
    private void Start() {
        _respawnController = GameObject.FindObjectOfType<RespawnController>();

        _checkpointRenderer = GetComponent<Renderer>();
        _checkpointRenderer.enabled = true;
        _checkpointRenderer.sharedMaterial = _materials[0];

        _checkpointCollider = GetComponent<Collider>();
        _checkpointCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            ChangeCheckpointMaterial();
            UpdatePlayerRespawn();
            this.enabled = false;
            _checkpointCollider.enabled = false;
            _whenPlayerPass.Invoke();
        }
    }


    // --- Functions ---
    private void ChangeCheckpointMaterial() {
        _checkpointRenderer.sharedMaterial = _materials[1];
    }

    private void UpdatePlayerRespawn() {
        _respawnController.UpdateRespawnPoint(this.transform);
    }

}
