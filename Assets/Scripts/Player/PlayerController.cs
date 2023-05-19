using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] private float _characterSpeed, _smoothTimeRotation, _gravityMultiplier, _jumpStrength;
    [SerializeField] private int _numberJumpsAllowed;
    private CharacterController _characterController;
    private Vector2 _playerInput;
    private Vector3 _inputDirection;
    private float _smoothCurrentVelocity;
    private float _fallVelocity;
    private float _gravity = -9.8f;
    private int _performedJumps;

    // --- Core Functions ---
    private void Awake() {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        ActivateGravityToBody();

        LookAtSmoothly();

        _characterController.Move(_inputDirection * _characterSpeed * Time.deltaTime);
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


    private void LookAtSmoothly() {
        if(_playerInput.sqrMagnitude == 0) return;

        float angleToTarget = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg;
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
