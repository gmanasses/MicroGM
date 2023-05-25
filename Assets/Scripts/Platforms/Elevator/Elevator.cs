using UnityEngine;

public class Elevator : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Transform _platformTransform, _arrivalPointTransform;
    [SerializeField, Range(0, 0.2f)] private float _elevatorSpeed;
    private PlayerController _playerController;
    private Vector3 _startPlatformPosition;
    private bool _downElevator;
    private float _tParam;


    // --- Core Fuctions ---
    private void OnEnable() {
        _startPlatformPosition = _platformTransform.position;
        _downElevator = true;
        _tParam = 0;

        _playerController = GameObject.FindObjectOfType<PlayerController>();
        _playerController.transform.SetParent(_platformTransform);
        _playerController.enabled = false;
    }

    private void Update() {
        if(_downElevator) {
            Down();
        }
    }


    // --- Fuctions ---
    public void EnableElevator() {
        this.enabled = true;
    }

    private void Down() {
        _tParam += Time.deltaTime * _elevatorSpeed;
        _platformTransform.position = Vector3.Lerp(_startPlatformPosition, _arrivalPointTransform.position, _animationCurve.Evaluate(_tParam));

        if(_platformTransform.position == _arrivalPointTransform.position) {
            this.enabled = false;
            _playerController.transform.SetParent(null);
            _playerController.enabled = true;
        }
    }

}
