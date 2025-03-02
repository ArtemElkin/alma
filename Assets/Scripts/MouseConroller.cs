using UnityEngine;

public class MouseConroller : MonoBehaviour, IService
{
    public bool blockMouse = false;
    [SerializeField] private float _scrollRate;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private PinMover _pinMover;
    private PinCreator _pinCreator;
    private PinViewer _pinViewer;
    private bool _isHolding = false;
    private float _timeHolded = 0f, _timeHoldedToMove = 0.75f;
    private Camera _camera;
    private Transform _holdingTF;
    private Vector2 _startViewportMousePos, _currentViewportMousePos;
    private Vector2 _startWorldMousePos, _currentWorldMousePos;

    private void Start()
    {
        _pinViewer = ServiceLocator.Current.Get<PinViewer>();
        _pinCreator = ServiceLocator.Current.Get<PinCreator>();
        _camera = Camera.main;
        _scrollRate = 0.5f;

    }
    private void Update()
    {
        if (blockMouse)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _isHolding = false;
            _timeHolded = 0f;

            _startWorldMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _startViewportMousePos  = _camera.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            _currentWorldMousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _currentViewportMousePos = _camera.ScreenToViewportPoint(Input.mousePosition);;
            if (_isHolding)
                _pinMover.MovePin(_holdingTF, _currentWorldMousePos);
            else
            {
                _cameraController.MoveCamera(_startWorldMousePos, _currentWorldMousePos);
                CheckForHolding();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (_isHolding)
            {
                _pinMover.UpdatePinPosition(_holdingTF);
            }
            if (!_isHolding && Vector2.Distance(_startViewportMousePos, _currentViewportMousePos) <= 0.01f)
            {
                RaycastHit2D hit = Physics2D.Raycast(_startWorldMousePos, transform.forward);
                if (hit.collider == null)
                    _pinCreator.SpawnPin(_startWorldMousePos.x, _startWorldMousePos.y);
                else
                    _pinViewer.View(hit.collider.GetComponent<Pin>());
            }
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            _cameraController.ResizeCamera(Input.mouseScrollDelta.y, _scrollRate);
        }
    }
    private void CheckForHolding()
    {
        RaycastHit2D hit = Physics2D.Raycast(_currentWorldMousePos, transform.forward);
        if (hit.collider != null)
        {
            _timeHolded += Time.deltaTime;
            if (_timeHolded >= _timeHoldedToMove)
            {
                _isHolding = true;
                _holdingTF = hit.collider.transform;
            }
        }
    }
}
