using UnityEngine;

public class Arrow : MonoBehaviour {

    // --- Private Declarations ---
    [SerializeField] float _arrowSpeed;
    [SerializeField] private bool _canMove = false;
    private Transform _arrowTransf, _spawnPoint, _endWall;
    private Vector3 _endPoint;
    private float _tParam = 0f;


    // --- Core Functions ---
    private void Start() {
        _arrowTransf = GetComponent<Transform>();
        _spawnPoint = this.transform.parent;
        _endWall = GameObject.FindGameObjectWithTag("ArrowEnd").transform;

        _endPoint = new Vector3(_endWall.position.x, _spawnPoint.position.y, _spawnPoint.position.z);
    }

    private void Update() {
        print(_canMove);
        if(_canMove) {
            MoveToEndWall();
        }
    }


    // --- Functions ---
    private void MoveToEndWall() {
        _tParam += Time.deltaTime * _arrowSpeed * 0.01f;
        _arrowTransf.position = Vector3.Lerp(_spawnPoint.position, _endPoint, _tParam);

        if(_arrowTransf.position == _endPoint) {
            ResetArrow();
        }
    }

    private void ResetArrow() {
        SetMove(false);
        _tParam = 0f;
        _arrowTransf.position = _spawnPoint.position;
    }

    public void SetMove(bool canMove) {
        this._canMove = canMove;
    }

}
