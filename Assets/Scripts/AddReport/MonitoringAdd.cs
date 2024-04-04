using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MonitoringAdd : ReportAdd
{
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_InputField _weightField;
    [SerializeField] private TMP_InputField _engineTemperatureField;
    [SerializeField] private TMP_InputField _airPressureField;
    [SerializeField] private TMP_InputField _fuelConsumptionField;
    [SerializeField] private CheckButton _balanceCheck;
    [SerializeField] private Transform _container;
    [SerializeField] private MonitoringReport _template;
    [SerializeField] private Button _addButton;
    [SerializeField] private ReportsDisplay _scrollView;
    private CheckButton.Status _status;

    private event UnityAction<string> OnWeightDeselect;
    private event UnityAction<string> OnEngineTemperatureDeselect;
    private event UnityAction<string> OnAirPressureDeselect;
    private event UnityAction<string> OnFuelConsumptionDeselect;

    private void Awake()
    {
        _balanceCheck.OnCheckButtonClick += ChangeBalance;

        OnWeightDeselect += ChangeWeightText;
        OnEngineTemperatureDeselect += ChangeEngineTemperatureText;
        OnAirPressureDeselect += ChangeAirPressureText;
        OnFuelConsumptionDeselect += ChangeFuelConsumptionText;
    }

    private void Start()
    {
        _addButton.onClick.AddListener(AddMonitoring);

        _weightField.onDeselect.AddListener(OnWeightDeselect);
        _engineTemperatureField.onDeselect.AddListener(OnEngineTemperatureDeselect);
        _airPressureField.onDeselect.AddListener(OnAirPressureDeselect);
        _fuelConsumptionField.onDeselect.AddListener(OnFuelConsumptionDeselect);
    }

    private void OnDisable()
    {       
        _nameField.text = "";
        _weightField.text = "";
        _engineTemperatureField.text = "";
        _airPressureField.text = "";
        _fuelConsumptionField.text = "";
    }

    private void OnDestroy()
    {
        _balanceCheck.OnCheckButtonClick -= ChangeBalance;
        OnWeightDeselect -= ChangeWeightText;
        OnEngineTemperatureDeselect -= ChangeEngineTemperatureText;
        OnAirPressureDeselect -= ChangeAirPressureText;
        OnFuelConsumptionDeselect -= ChangeFuelConsumptionText;
    }

    private void AddMonitoring()
    {
        MonitoringReport monitoring = Instantiate(_template, _container);
        monitoring.Render(_nameField.text, _weightField.text, _engineTemperatureField.text, _airPressureField.text, _fuelConsumptionField.text, _status);

        _scrollView.TryChangeScrollVisible();
        gameObject.GetComponent<PanelAnimation>().CloseRequest();
    }

    // Обновляем статус при нажатии на Good/Violated
    private void ChangeBalance(CheckButton.Status status)
    {
        _status = status;
    }

    // Методы, чтобы в поля автоматически дописывались меры измерения

    private void ChangeWeightText(string weight)
    {
        if (IsNeedChange(weight, "kg"))
            _weightField.text = $"{weight} kg";
    }

    private void ChangeEngineTemperatureText(string engineTemperature)
    {
        if (IsNeedChange(engineTemperature, "°C"))
            _engineTemperatureField.text = $"{engineTemperature} °C";
    }

    private void ChangeAirPressureText(string airPressure)
    {
        if (IsNeedChange(airPressure, "GPa"))
            _airPressureField.text = $"{airPressure} GPa";
    }

    private void ChangeFuelConsumptionText(string fuelConsumption)
    {
        if (IsNeedChange(fuelConsumption, "g/pass-km"))
            _fuelConsumptionField.text = $"{fuelConsumption} g/pass-km";
    }

    // Проверяем, нужно ли ваще добавлять эти меры измерения в данном случае
    private bool IsNeedChange(string input, string pattern)
    {
        if (input == "")
            return false;

        if (Regex.IsMatch(input, pattern))
            return false;

        return true;
    }
}
