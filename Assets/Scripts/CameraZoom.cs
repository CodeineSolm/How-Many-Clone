using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private QuestionHandler _questionHandler;
    [SerializeField] private float _zoomSpeed = 0.005f;
    [SerializeField] private float _zoomShiftByY = 1f;
    [SerializeField] private float _zoomShiftByZ = -7f;
    
    private bool _isActive;
    private Camera _camera;
    private Vector3 _startPosition;


    public void Zoom()
    {
        Vector3 target = new Vector3(_player.transform.position.x, _player.transform.position.y + _zoomShiftByY, _player.transform.position.z + _zoomShiftByZ);
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, target, _zoomSpeed);
    }

    private void Awake()
    {
        _startPosition = new Vector3(0, 0, 0);
        _isActive = false;
        _camera = Camera.main;
        Reset();
    }

    private void Update()
    {
        if (_isActive)
        {
            Zoom();
        }
    }

    private void Reset()
    {
        _camera.transform.position = _startPosition;
    }

    private void OnEnable()
    {
        _questionHandler.PlayerSurvived += OnPlayerSurvived;
    }

    private void OnDisable()
    {
        _questionHandler.PlayerSurvived -= OnPlayerSurvived;
    }

    private void OnPlayerSurvived()
    {
        _isActive = true;
    }
}
