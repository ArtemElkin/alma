using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PinViewer : MonoBehaviour, IService
{
    [SerializeField] private Popup popup;
    [SerializeField] private TMP_InputField tmp_name, tmp_name2, tmp_info;
    [SerializeField] private Button _deleteBtn;
    [SerializeField] private SaveLoadManager saveLoadManager;
    private Pin _currentPin;
    private void OnEnable()
    {
        _deleteBtn.onClick.AddListener(OnDeleteBtnHandler);
    }
    private void OnDisable()
    {
        _deleteBtn.onClick.RemoveAllListeners();
    }
    public void View(Pin pin)
    {
        _currentPin = pin;
        if(!string.IsNullOrEmpty(pin.Name))
            tmp_name.text = pin.Name;
        else
            tmp_name.text = "Название";
        if(!string.IsNullOrEmpty(pin.Info))
            tmp_info.text = pin.Info;
        else
            tmp_info.text = "Описание";
        popup.Show();
    }
    public void EditPinName(string name)
    {
        tmp_name.text = name;
        tmp_name2.text = name;
        _currentPin.SetName(name);

        saveLoadManager.SaveGame();
    }
    public void EditPinInfo(string info)
    {
        _currentPin.SetInfo(info);
        saveLoadManager.SaveGame();
    }
    public void OnDeleteBtnHandler()
    {
        ServiceLocator.Current.Get<PinCreator>().DeletePin(_currentPin);
        saveLoadManager.SaveGame();
    }
}
