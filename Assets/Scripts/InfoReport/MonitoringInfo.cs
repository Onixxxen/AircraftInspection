using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonitoringInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private TMP_Text _weightField;
    [SerializeField] private TMP_Text _engineTemperatureField;
    [SerializeField] private TMP_Text _airPressureField;
    [SerializeField] private TMP_Text _fuelConsumptionField;
    [SerializeField] private TMP_Text _balanceField;
    [SerializeField] private Image _goodBalanceImage;
    [SerializeField] private Image _violatedBalanceImage;
    [SerializeField] private MonitoringEdit _monitoringEdit;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private ReportsDisplay _scrollView;

    private CheckButton.Status _balance;

    private MonitoringReport _monitoringReport;

    private void Start()
    {
        _editButton.onClick.AddListener(OpenEditRequest);
        _deleteButton.onClick.AddListener(DeleteMonitoringRequest);
    }

    private void OnDisable()
    {
        _nameField.text = "";
        _weightField.text = "";
        _engineTemperatureField.text = "";
        _airPressureField.text = "";
        _fuelConsumptionField.text = "";
        _balanceField.text = "";

        _goodBalanceImage.gameObject.SetActive(false);
        _violatedBalanceImage.gameObject.SetActive(false);
    }

    public void OpenInfo(MonitoringReport monitoring, string name, string weight, string engineTemperature, string airPressure, string fuelConsumption, CheckButton.Status balance)
    {
        _monitoringReport = monitoring;

        _nameField.text = name;
        _weightField.text = weight;
        _engineTemperatureField.text = engineTemperature;
        _airPressureField.text = airPressure;
        _fuelConsumptionField.text = fuelConsumption;
        _balance = balance;
        _balanceField.text = _balance.ToString();

        // ѕровер€ем какой установлен статус и мен€ем цвет текста и иконку в зависимости от него

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

        gameObject.GetComponent<PanelAnimation>().OpenRequest();
    }

    private void OpenEditRequest()
    {
        _monitoringEdit.OpenEdit(_monitoringReport, _weightField.text, _engineTemperatureField.text, _airPressureField.text, _fuelConsumptionField.text, _balance);
    }

    private void DeleteMonitoringRequest()
    {
        StartCoroutine(DeleteMonitoring());
    }

    private IEnumerator DeleteMonitoring()
    {
        Destroy(_monitoringReport.gameObject);

        yield return new WaitForSeconds(0.1f); // чтобы программа успела удалить отчет и не возникло никаких ошибок

        _scrollView.TryChangeScrollVisible();
        GetComponent<PanelAnimation>().CloseRequest();
    }
}
