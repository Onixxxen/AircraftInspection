using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MonitoringEdit : MonoBehaviour
{
    [SerializeField] private TMP_InputField _weightField;
    [SerializeField] private TMP_InputField _engineTemperatureField;
    [SerializeField] private TMP_InputField _airPressureField;
    [SerializeField] private TMP_InputField _fuelConsumptionField;
    [SerializeField] private TMP_Text _balanceField;
    [SerializeField] private Image _goodBalanceImage;
    [SerializeField] private Image _violatedBalanceImage;
    [SerializeField] private CheckButton _balanceCheck;
    [SerializeField] private MonitoringInfo _monitoringInfo;
    [SerializeField] private GameObject _changeBalancePanel;
    [SerializeField] private Button _statusSaveButton;
    [SerializeField] private Button _saveButton;

    private CheckButton.Status _balance;

    private MonitoringReport _monitoringReport;

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
        _saveButton.onClick.AddListener(SaveInfo);
        _statusSaveButton.onClick.AddListener(ChangeBalanceStatus);

        _weightField.onDeselect.AddListener(OnWeightDeselect);
        _engineTemperatureField.onDeselect.AddListener(OnEngineTemperatureDeselect);
        _airPressureField.onDeselect.AddListener(OnAirPressureDeselect);
        _fuelConsumptionField.onDeselect.AddListener(OnFuelConsumptionDeselect);
    }

    private void OnDisable()
    {
        _weightField.text = "";
        _engineTemperatureField.text = "";
        _airPressureField.text = "";
        _fuelConsumptionField.text = "";
        _balanceField.text = "";

        _goodBalanceImage.gameObject.SetActive(false);
        _violatedBalanceImage.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _balanceCheck.OnCheckButtonClick -= ChangeBalance;

        OnWeightDeselect -= ChangeWeightText;
        OnEngineTemperatureDeselect -= ChangeEngineTemperatureText;
        OnAirPressureDeselect -= ChangeAirPressureText;
        OnFuelConsumptionDeselect -= ChangeFuelConsumptionText;
    }

    public void OpenEdit(MonitoringReport monitoring, string weight, string engineTemperature, string airPressure, string fuelConsumption, CheckButton.Status balance)
    {
        _monitoringReport = monitoring;

        _weightField.text = weight;
        _engineTemperatureField.text = engineTemperature;
        _airPressureField.text = airPressure;
        _fuelConsumptionField.text = fuelConsumption;
        _balance = balance;

        UpdateBalanceField();

        gameObject.gameObject.GetComponent<PanelAnimation>().OpenRequest();
    }

    private void SaveInfo()
    {
        _monitoringReport.ChangeInfo(_weightField.text, _engineTemperatureField.text, _airPressureField.text, _fuelConsumptionField.text, _balance);
        _monitoringInfo.GetComponent<PanelAnimation>().CloseRequest();
        gameObject.GetComponent<PanelAnimation>().CloseRequest();
    }

    // Обновляем статус при нажатии на Good/Violated
    private void ChangeBalance(CheckButton.Status status)
    {
        _balance = status;
    }

    private void ChangeBalanceStatus()
    {
        UpdateBalanceField();
        _changeBalancePanel.GetComponent<PanelAnimation>().CloseRequest();
    }

    // Проверяем какой установлен статус и меняем цвет текста и иконку в зависимости от него
    private void UpdateBalanceField()
    {
        _balanceField.text = _balance.ToString();

        if (_balance == CheckButton.Status.Good)
        {
            _balanceField.color = new Color32(50, 215, 74, 255);
            _goodBalanceImage.gameObject.SetActive(true);
            _violatedBalanceImage.gameObject.SetActive(false);
        }
        else
        {
            _balanceField.color = new Color32(220, 38, 38, 255);
            _goodBalanceImage.gameObject.SetActive(false);
            _violatedBalanceImage.gameObject.SetActive(true);
        }
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
