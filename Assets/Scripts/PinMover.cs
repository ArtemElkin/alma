using UnityEngine;

public class PinMover : MonoBehaviour
{
    public void MovePin(Transform pinTF, Vector2 newPosition)
    {
        pinTF.position = newPosition;
    }
    public void UpdatePinPosition(Transform pinTF)
    {
        var pin = pinTF.GetComponent<Pin>();
        pin.SetPosition(pinTF.position.x, pinTF.position.y);
    }
}
