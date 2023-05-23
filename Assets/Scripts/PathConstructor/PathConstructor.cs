using System.Collections;
using UnityEngine;

public class PathConstructor : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private GameObject _paths;
    [SerializeField, Range(0, 5)] private float _timeBetweenPaths;
    [SerializeField] private PathBlock[] _pathblocks;


    // --- Core Functions ---
    private void Start() {
        _pathblocks = _paths.GetComponentsInChildren<PathBlock>(true);
    }


    // --- Functions ---
    public void StartConstruction() {
        StartCoroutine(Build());
    }

    public IEnumerator Build() {
        foreach (PathBlock pathBlock in _pathblocks) {
            pathBlock.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(_timeBetweenPaths);
        }
    }

}
