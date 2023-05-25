using UnityEngine;

public class InterfaceController : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private GameObject _interactionPanel;


    // --- Fuctions ---
    public void EnableOrDisableInteractionPanel(bool enable) {
        _interactionPanel.SetActive(enable);
    }

}
