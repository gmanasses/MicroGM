using System.Collections;
using UnityEngine;

public class MapController : MonoBehaviour {

    // --- Private Declarations ---
    [Header("Level 1")]
    [SerializeField] private GameObject[] _scene1;
    [SerializeField] private GameObject _finishLevel1;


    // --- Core Fuctions ---
    private void Start() {
        _finishLevel1.SetActive(false);
    }


    // --- Fuctions ---
    public void DisableScene1() {
        StartCoroutine(DisableWithDelay());
    }

    private IEnumerator DisableWithDelay() {
        foreach (GameObject obj in _scene1) {
            obj.SetActive(false);
            yield return new WaitForSeconds(2);
        }
    }

    public void EnableFinishLevel1() {
        _finishLevel1.SetActive(true);
    }

}
