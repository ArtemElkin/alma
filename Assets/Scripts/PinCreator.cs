using System.Collections.Generic;
using UnityEngine;

public class PinCreator : MonoBehaviour, IService
{
    public GameObject pinPrefab;
    private List<Pin> _pins;
    public void SpawnPin(float x, float y, string name = null, string details = null)
    {
        Vector3 position = new Vector3(x, y, transform.position.z);
        Pin pin = Instantiate(pinPrefab, position, Quaternion.identity, transform).GetComponent<Pin>();
        pin.x = x;
        pin.y = y;
        if (name != null)
            pin.Name = name;
        if (details != null)
            pin._info = details;
        AddToList(pin);
    }
    public void AddToList(Pin pin)
    {
        if (_pins == null)
            _pins = new List<Pin>();
        _pins.Add(pin);
    }
    public List<Pin> GetPins() => _pins;
    public void DeletePin(Pin pin)
    {
        Debug.Log(_pins.Contains(pin));
        _pins.Remove(pin);
        pin.gameObject.SetActive(false);
        Debug.Log("Deleted successfully");
    }
}
