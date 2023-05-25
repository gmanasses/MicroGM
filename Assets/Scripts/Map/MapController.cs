using System.Collections;
using UnityEngine;

public class MapController : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private GameObject[] _scene1;


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

}
