using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    [SerializeField] private PinCreator _pinCreator;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private MouseConroller _mouseConroller;

    private void Awake()
    {
        ServiceLocator.Initialize();
        ServiceLocator.Current.Register<PinCreator>(_pinCreator);
        ServiceLocator.Current.Register<CameraController>(_cameraController);
        ServiceLocator.Current.Register<MouseConroller>(_mouseConroller);
    }
}
