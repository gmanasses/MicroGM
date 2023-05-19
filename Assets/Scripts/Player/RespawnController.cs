using UnityEngine;

public class RespawnController : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField, Range(-15, -4)] private float _heightToRespawn;
    [SerializeField] private Transform _spawnPoint;


    private void FixedUpdate() {
        if (this.transform.position.y < _heightToRespawn) {
            this.transform.position = new Vector3(_spawnPoint.position.x, _spawnPoint.position.y, _spawnPoint.position.z);
        }
    }

}
