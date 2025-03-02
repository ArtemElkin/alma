using System;
using UnityEngine;

public sealed class CameraController : MonoBehaviour, IService
{
    [SerializeField] private SpriteRenderer _backgroundSR;
    private float _backgroundWidth, _backgroundHeight;
    private float _leftBorderX, _rightBorderX;
    private float _upperBorderY, _bottomBorderY;
    private float _maxOrthographicSize, _maxCameraWidth, _maxCameraHeight, _newOrthograpgicSize;
    private Camera _camera;
    public event Action CameraSizeChanged;
    
    private void Awake()
    {
        _camera = Camera.main;
        CalculateCameraSizeLimits();
        CalculateMovementBoundaries();

    }
    public float GetCameraScale()
    {
        Vector2 cameraSize = GetCameraSize();
        float cameraWidth = cameraSize.x;
        float cameraHeight = cameraSize.y;
        if (_camera.aspect >= 1)
            return cameraWidth / _backgroundWidth;
        else
            return cameraHeight / _backgroundHeight;
    }
    public void MoveCamera(Vector2 startMousePos, Vector2 currentMousePos)
    {
        Vector2 offset = currentMousePos - startMousePos;
        transform.position = ClampCameraPosition(transform.position.x - offset.x, transform.position.y - offset.y);
    }
    public void ResizeCamera(float mouseScrollDeltaY, float _scrollRate)
    {
        if (mouseScrollDeltaY != 0)
        {
            _newOrthograpgicSize = _camera.orthographicSize - mouseScrollDeltaY * _scrollRate;
            CalculateMovementBoundaries();
            transform.position = ClampCameraPosition(transform.position.x, transform.position.y);
            _camera.orthographicSize = Mathf.Clamp(_newOrthograpgicSize, 2f, _maxOrthographicSize);
            CameraSizeChanged?.Invoke();
        }
    }
    private Vector2 GetCameraSize()
    {
        float cameraHeight = _camera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * _camera.aspect;
        return new Vector2(cameraWidth, cameraHeight);
    }
    private void CalculateCameraSizeLimits()
    {
        _backgroundWidth = _backgroundSR.bounds.size.x;
        _backgroundHeight = _backgroundSR.bounds.size.y;
        if (_camera.aspect >= 1)
        {
            _maxCameraWidth = _backgroundWidth;
            _maxCameraHeight = _maxCameraWidth / _camera.aspect;
        } else
        {
            _maxCameraHeight = _backgroundHeight;
            _maxCameraWidth = _maxCameraHeight * _camera.aspect;
        }
        _maxOrthographicSize = _maxCameraHeight / 2f;
    }
    private Vector3 ClampCameraPosition(float x, float y)
    {
        float clampedPosX = Mathf.Clamp(x, _leftBorderX, _rightBorderX);
        float clampedPosY = Mathf.Clamp(y, _bottomBorderY, _upperBorderY);
        return new Vector3(clampedPosX, clampedPosY, transform.position.z);
    }
    private void CalculateMovementBoundaries()
    {
        Vector2 cameraSize = GetCameraSize();
        float cameraWidth = cameraSize.x;
        float cameraHeight = cameraSize.y;
        _leftBorderX = - _backgroundWidth / 2f + cameraWidth / 2f;
        _rightBorderX = _backgroundWidth / 2f - cameraWidth / 2f;
        _bottomBorderY = - _backgroundHeight / 2f + cameraHeight / 2f;
        _upperBorderY = _backgroundHeight / 2f - cameraHeight / 2f;
    }
}
