using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundController : MonoBehaviour {

    // --- Private Declarations ---
    [Header("Buttons")]
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private int _angleButtonRotation;
    [SerializeField, Range(0, 25)] private int _movementButton;
    [SerializeField, Range(0, 1)] private float _animationTimeButton;
    private List<Vector3> _startButtonPositions;


    // --- Core Fuctions ---
    private void Start() {
        _startButtonPositions = new List<Vector3>();

        foreach (GameObject button in _buttons) {
            _startButtonPositions.Add(button.transform.position);
        }
    }


    // --- Fuctions ---
    public void PutButtonOnClickablePosition(int index) {
        LeanTween.rotateX(_buttons[index], _angleButtonRotation, _animationTimeButton);

        Vector3 teste = new Vector3(_startButtonPositions[index].x + _movementButton, _startButtonPositions[index].y, _startButtonPositions[index].z);
        LeanTween.move(_buttons[index], teste, _animationTimeButton);

        PutOthersButtonsOnStartPosition(index);
    }

    private void PutOthersButtonsOnStartPosition(int index) {
        for(int i=0; i<_buttons.Length; i++) {
            if (i == index) continue;

            LeanTween.rotateX(_buttons[i], 0, _animationTimeButton);
            LeanTween.move(_buttons[i], _startButtonPositions[i], _animationTimeButton);
        }
    }

    public void PutSettingsBlock() {

    }

}
