using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitoringReport : MonoBehaviour, IChangesScrollView
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private List<Button> _openButtons;

    private string _name;
    private string _weight;
    private string _engineTemperature;
    private string _airPressure;
    private string _fuelConsumption;
    private CheckButton.Status _balance;

    private MonitoringInfo _monitoringInfo;

    private void Start()
    {
        _monitoringInfo = FindObjectOfType<MonitoringInfo>(true);        

        for (int i = 0; i < _openButtons.Count; i++)
            _openButtons[i].onClick.AddListener(OpenInfoRequest);
    }

    public void Render(string name, string weight, string engineTemperature, string airPressure, string fuelConsumption, CheckButton.Status balance)
    {
        _name = name;
        _weight = weight;
        _engineTemperature = engineTemperature;
        _airPressure = airPressure;
        _fuelConsumption = fuelConsumption;
        _balance = balance;

        _nameText.text = _name;
    }

    // Открываем информацию об отчете и передаем туда данные
    public void OpenInfoRequest()
    {
        _monitoringInfo.OpenInfo(this, _name, _weight, _engineTemperature, _airPressure, _fuelConsumption, _balance);
    }

    public void ChangeInfo(string weight, string engineTemperature, string airPressure, string fuelConsumption, CheckButton.Status balance)
    {
        _weight = weight;
        _engineTemperature = engineTemperature;
        _airPressure = airPressure;
        _fuelConsumption = fuelConsumption;
        _balance = balance;

        _nameText.text = _name;
    }
}
