using UnityEngine;

public class ArrowTrap : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private GameObject _arrowPrefab;


    // --- Core Functions ---
    private void Start() {
        foreach(Transform transf in _spawnPositions) {
            Instantiate(_arrowPrefab, transf);
        }
    }

}
