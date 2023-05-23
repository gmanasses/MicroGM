using UnityEngine;

public class PathBlock : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private Transform _pathBlock, _firstTargetTransform, _secondTargetTransform;
    [SerializeField] private float _moveSpeed;
    private Vector3 _targetPosition, _startPosition;
    private float _tParam = 0f;
    private bool _canBuild = false;
    private bool _stage1 = true;


    // --- Core Functions ---
    private void Start() {
        _targetPosition = _firstTargetTransform.position;
        _startPosition = _pathBlock.position;
    }

    private void Update() {
        if(_canBuild && _stage1) {
            _tParam += Time.deltaTime * _moveSpeed;
            _pathBlock.position = Vector3.Lerp(_startPosition, _targetPosition, _animationCurve.Evaluate(_tParam));

            if(_pathBlock.position == _firstTargetTransform.position) {
                _targetPosition = _secondTargetTransform.position;
                _startPosition = _firstTargetTransform.position;
                _stage1 = false;
                _tParam = 0;
            }
        } else if(_canBuild && !_stage1) {
            _tParam += Time.deltaTime * _moveSpeed;
            _pathBlock.position = Vector3.Lerp(_startPosition, _targetPosition, _animationCurve.Evaluate(_tParam));

            if(_pathBlock.position == _secondTargetTransform.position) {
                this.enabled = false;
            }
        }
    }

    private void OnEnable() {
        _canBuild = true;
    }

}
