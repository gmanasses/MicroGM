using UnityEngine;

public class RespawnController : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField, Range(-15, -4)] private float _heightToRespawn;
    [SerializeField] private Transform _spawnPoint;


    // --- Core Functions ---
    private void FixedUpdate() {
        if (this.transform.position.y < _heightToRespawn) {
            DoRespawn();
        }
    }


    // --- Functions ---
    public void UpdateRespawnPoint(Transform checkpointTransform) {
        this._spawnPoint = checkpointTransform;
    }

    public void DoRespawn() {
        this.transform.position = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y, _spawnPoint.position.z);
    }

}
