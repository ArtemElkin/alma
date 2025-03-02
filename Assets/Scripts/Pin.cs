using UnityEngine;

public class Pin : MonoBehaviour
{
    public string Name { get; set; }
    public string Info { get; set; }
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
        this.Info = info;
    }
    public void SetName(string name)
    {
        this.Name = name;
    }
    public void SetInfo(string info)
    {
        this.Info = info;
    }
    public void SetPosition(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}
