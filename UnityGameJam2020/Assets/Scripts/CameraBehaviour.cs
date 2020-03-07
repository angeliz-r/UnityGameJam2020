using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Camera _mainCam;
    private float _gridSize;

    private void Start() {
        _mainCam = Camera.main;
        UpdateCameraFocus(9);
    }

    private void Update() {
        if(_mainCam.orthographicSize != _gridSize)
        _mainCam.orthographicSize = Mathf.Lerp(_mainCam.orthographicSize, _gridSize + 1, 10f);
    }

    public void UpdateCameraFocus(int size) {
        var c = Mathf.Floor((float)size / 2);
        _gridSize = c;
        Vector2 p = new Vector2(c, c);
        _mainCam.orthographicSize = c + 1;
        _mainCam.transform.LeanMove(p, 0.5f);
    }
}
