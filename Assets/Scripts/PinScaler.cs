using UnityEngine;

public class PinScaler : MonoBehaviour
{
    private CameraController _cameraController;
    private float _scaleRatio;
    private Vector3 _startScale;

    private void Start()
    {
        _cameraController = ServiceLocator.Current.Get<CameraController>();
        _startScale = transform.localScale;
        CalculateScale();
    }
    private void OnEnable()
    {
        _cameraController.CameraSizeChanged += CameraSizeChanged;
    }
    private void OnDisable()
    {
        _cameraController.CameraSizeChanged -= CameraSizeChanged;
    }

    private void CameraSizeChanged()
    {
        CalculateScale();
    }

    
    private void CalculateScale()
    {
        _scaleRatio = _cameraController.GetCameraScale();
        transform.localScale = _startScale * _scaleRatio;
    }
}
