using UnityEngine;

public class Pin : MonoBehaviour
{
    public string Name {get; set;}
    public string _info;
    public float x, y;
    public Pin(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
    public Pin(float x, float y, string name, string info)
    {
        this.x = x;
        this.y = y;
        this.Name = name;
        this._info = info;
    }
    public void SetName(string name)
    {
        this.Name = name;
    }
    public void SetInfo(string info)
    {
        this._info = info;
    }
    public void SetPosition(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}
