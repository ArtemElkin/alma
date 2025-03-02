using UnityEngine;
//Интерфейс адаптера
public interface ISaveLoader
{
    void LoadData();
    void SaveData();
}



public sealed class SaveLoadManager : MonoBehaviour
{
    
    private ISaveLoader[] saveLoaders;
    private void Awake()
    {
        saveLoaders = new ISaveLoader[]
        { 
            new PinsSaveLoader()
        };
        LoadGame();
    }
    [ContextMenu("Load Game")]
    public void LoadGame()
    {
        Repository.LoadState();

        foreach (var saveLoader in this.saveLoaders)
        {
            saveLoader.LoadData();
        }
    }
    [ContextMenu("Save Game")]
    public void SaveGame()
    {
        foreach (var saveLoader in this.saveLoaders)
        {
            saveLoader.SaveData();
        }
        
        Repository.SaveState();
    }
}