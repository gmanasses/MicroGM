using UnityEngine;
using UnityEngine.Events;

public class ActivationButton : MonoBehaviour {

    // --- Private Declarations ---
    #region Cube Button
    [SerializeField] private Transform _buttonTransform, _buttonPressedTransform;
    [SerializeField] private Renderer _buttonRenderer;
    [SerializeField] private Color _startColor, _endColor;
    [SerializeField] private float _rotationSpeed, _moveSpeed;
    private Material _buttonMaterial;
    private bool _canSpin = true;
    private string _stage;
    #endregion

    #region Interface Connection
    private InterfaceController _interfaceController;
    #endregion

    #region General
    [SerializeField] private UnityEvent _whenButtonPressed, _whenPlayerInteracts;
    private bool _wasPressed = false;
    private bool _canCheckInteract = false;
    #endregion


    // --- Core Functions ---
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            _canCheckInteract = true;

            if(!_wasPressed) _interfaceController.EnableOrDisableInteractionPanel(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            _canCheckInteract = false;
            if(!_wasPressed) _interfaceController.EnableOrDisableInteractionPanel(false);
        }
    }

    private void Start() {
        _interfaceController = GameObject.FindObjectOfType<InterfaceController>();

        _buttonMaterial = _buttonRenderer.material;
        _buttonMaterial.color = _startColor;

        _stage = "rotation";
    }

    private void Update() {
        SpinButton();

        if(_canCheckInteract) {
            CheckInteraction();

            if(_wasPressed) {
                _canSpin = false;
                _canCheckInteract = false;
            }

        } else if (_wasPressed) {
            switch (_stage) {
                case "rotation":
                    _buttonTransform.eulerAngles = Vector3.Lerp(_buttonTransform.eulerAngles, Vector3.zero, 8 * Time.deltaTime);

                    if (_buttonTransform.eulerAngles == Vector3.zero) {
                        _stage = "pressing";
                    }
                break;

                case "pressing":
                    if (!(_buttonTransform.position == _buttonPressedTransform.position)) _buttonTransform.position = Vector3.MoveTowards(_buttonTransform.position, _buttonPressedTransform.position, Time.deltaTime);
                    if (!(_buttonMaterial.color == _endColor)) _buttonMaterial.color = Color.Lerp(_buttonMaterial.color, _endColor, 5 * Time.deltaTime);

                    if ((_buttonTransform.position == _buttonPressedTransform.position) && (_buttonMaterial.color == _endColor)) {
                        _whenButtonPressed.Invoke();
                        this.enabled = false;
                    }
                break;
            }
        }
    }


    // --- Functions ---
    private void SpinButton() {
        if(_canSpin) {
            _buttonTransform.Rotate(0, _rotationSpeed * Time.deltaTime, 0, Space.World);
        }
    }

    private void CheckInteraction() {
        if(Input.GetKeyDown(KeyCode.E)) {
            _wasPressed = true;
            _canSpin = false;

            _whenPlayerInteracts.Invoke();

            _interfaceController.EnableOrDisableInteractionPanel(false);
        }
    }

}
