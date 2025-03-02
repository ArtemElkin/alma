using System.Collections.Generic;
using System;
//Данные для сохранения монет
[Serializable]
public struct PinData
{
    public string name, details;
    public float x,y;
}
public class PinsSaveLoader : ISaveLoader
{
    private PinCreator pinCreator;
    public PinsSaveLoader()
    {
        this.pinCreator = ServiceLocator.Current.Get<PinCreator>();
    }
    public void LoadData()
    {
        PinData[] dataSet = Repository.GetData<PinData[]>();
        int count = dataSet.Length;
        for(int i = 0; i < count; i++)
        {
            PinData pinData = dataSet[i];
            pinCreator.SpawnPin(pinData.x, pinData.y, pinData.name, pinData.details);
        }
    }

    public void SaveData()
    {
        List<Pin> pins = pinCreator.GetPins();
        int count = pins.Count;
        PinData[] dataset = new PinData[count]; 
        for(int i = 0; i < count; i++)
        {
            dataset[i] = new PinData{
                name = pins[i].Name,
                details = pins[i].Info,
                x = pins[i].x,
                y = pins[i].y
            };
        }
        Repository.SetData(dataset);

    }
}
