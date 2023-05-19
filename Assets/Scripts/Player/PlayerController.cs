using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    // --- Private Declarations ---
    #region Movements
    [SerializeField] private float _characterSpeed;
    private CharacterController _characterController;
    private Vector2 _playerInput;
    private Vector3 _inputDirection;
    #endregion
    #region Rotation
    [SerializeField] private float _smoothTimeRotation;
    private float _smoothCurrentVelocity;
    #endregion
    #region Gravity
    [SerializeField] private float _gravityMultiplier;
    private float _gravity = -9.8f;
    private float _fallVelocity;
    #endregion
    #region Jump
    [SerializeField] private float _jumpStrength;
    [SerializeField] private int _numberJumpsAllowed;
    private int _performedJumps;
    #endregion


    // --- Core Functions ---
    private void Awake() {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        ActivateGravityToBody();

        LookAtSmoothly();

        _characterController.Move(_inputDirection.ToIso() * _characterSpeed * Time.deltaTime);
    }


    // --- Functions ---
    public void Move(InputAction.CallbackContext context) {
        _playerInput = context.ReadValue<Vector2>();
        _inputDirection = new Vector3(_playerInput.x, 0, _playerInput.y);
    }

    public void Jump(InputAction.CallbackContext context) {
        if(!context.started) {
            return;
        } 
        
        if(!PlayerIsGrounded() && (_performedJumps >= _numberJumpsAllowed)) {
            return;
        } 
        
        if(_performedJumps == 0) {
            StartCoroutine(WaitForJumpClearance());
        } 

        _performedJumps++;
        _fallVelocity = _jumpStrength;
    }

    public void MegaJump(float megaJumpStrngth) {
        _performedJumps = _numberJumpsAllowed + 1;
        _fallVelocity = megaJumpStrngth;

        StartCoroutine(WaitForJumpClearance());
    }

    private void LookAtSmoothly() {
        if(_playerInput.sqrMagnitude == 0) return;

        float angleToTarget = Mathf.Atan2(_inputDirection.ToIso().x, _inputDirection.ToIso().z) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, angleToTarget, ref _smoothCurrentVelocity, _smoothTimeRotation);
        this.transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
    }

    private void ActivateGravityToBody() {
        if(PlayerIsGrounded() && _fallVelocity < 0) {
            _fallVelocity = -1;

        } else {
            _fallVelocity += _gravity * _gravityMultiplier * Time.deltaTime; 
        }

        _inputDirection.y = _fallVelocity;
    }
    
    private IEnumerator WaitForJumpClearance() {
        yield return new WaitUntil(() => !PlayerIsGrounded());
        yield return new WaitUntil(PlayerIsGrounded);

        _performedJumps = 0;
    }

    private bool PlayerIsGrounded() {
        return _characterController.isGrounded;
    }

}


public static class IsometricConversion {

    private static Matrix4x4 _isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    public static Vector3 ToIso(this Vector3 playerInput) => _isometricMatrix.MultiplyPoint3x4(playerInput);

}
