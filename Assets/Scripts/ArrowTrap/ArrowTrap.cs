using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private Transform[] _spawnPositions;
    [SerializeField] private GameObject _arrowPrefab;
    private List<Arrow> _arrows;
    private bool _canShootAll = true;


    // --- Core Functions ---
    private void Start() {
        _arrows = new List<Arrow>();

        foreach(Transform transf in _spawnPositions) {
            GameObject arrowObj = Instantiate(_arrowPrefab, transf);
            _arrows.Add(arrowObj.GetComponent<Arrow>());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && _canShootAll) {
            StartCoroutine(ActivateAllArrows());
        }
    }


    // --- Functions ---
    private IEnumerator ActivateAllArrows() {
        _canShootAll = false;
        yield return new WaitForSecondsRealtime(1);
        foreach(Arrow arrow in _arrows) {
            arrow.SetMove(true);
        }

        yield return new WaitForSecondsRealtime(2);
        _canShootAll = true;
    }
}
