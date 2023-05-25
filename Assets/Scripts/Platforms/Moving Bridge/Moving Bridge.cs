using UnityEngine;
using UnityEngine.Events;

public class MovingBridge : MonoBehaviour {

    // --- Private Declarations ---
    #region Bridge
    [Header("Bridge")]
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Transform _bridgeTransform, _arrivalPointTransform;
    [SerializeField] private UnityEvent _whenArrives;
    [SerializeField, Range(0, 5)] private float _bridgeMovementSpeed;
    private PlayerController _playerController;
    private Vector3 _startBridgePosition, _startBridgeRotation;
    private bool _canMoveBridge;
    private float _tParam, _tParamRotation;
    #endregion

    #region Button
    [Header("Button")]
    [SerializeField, Range(0, 5)] private float _buttonPlatformRotationSpeed;
    [SerializeField] private Transform _buttonPlatform, _targetRotationTransform;
    private Quaternion _startButtonRotation, _targetRotation;
    private bool _canRotateButton;
    private float _tParamButton;
    #endregion


    // --- Core Fuctions ---
    private void OnEnable() {
        _canRotateButton = true;
        _startButtonRotation = _buttonPlatform.rotation;
        _targetRotation = _targetRotationTransform.rotation;
        _tParamButton = 0;

        _startBridgePosition = _bridgeTransform.position;
        _startBridgeRotation = _bridgeTransform.eulerAngles;
        _tParam = 0;
        _tParamRotation = 0;

        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _playerController.transform.SetParent(_bridgeTransform);
        _playerController.enabled = false;
    }

    private void Update() {
        if (_canRotateButton) RotateButton();
        if (_canMoveBridge) MoveAndRotateBridge();
    }


    // --- Fuctions ---
    public void StartBridgeMovement() {
        this.enabled = true;
    }

    private void MoveAndRotateBridge() {
        _tParam += Time.deltaTime * _bridgeMovementSpeed; 
        _bridgeTransform.position = Vector3.Lerp(_startBridgePosition, _arrivalPointTransform.position, _animationCurve.Evaluate(_tParam));

        _tParamRotation += Time.deltaTime;
        _bridgeTransform.eulerAngles = Vector3.Lerp(_startBridgeRotation, _arrivalPointTransform.eulerAngles, _tParamRotation);

        if (_bridgeTransform.position == _arrivalPointTransform.position) {
            this.enabled = false;
            _playerController.transform.SetParent(null);
            _playerController.enabled = true;
            _whenArrives.Invoke();
        }
    }

    private void RotateButton() {
        _tParamButton += Time.deltaTime * _buttonPlatformRotationSpeed;
        _buttonPlatform.rotation = Quaternion.Lerp(_startButtonRotation, _targetRotation, _tParamButton);

        if(_buttonPlatform.rotation == _targetRotation) {
            _canMoveBridge = true;
            _canRotateButton = false;
        }
    }

}
