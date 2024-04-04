using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlightCheck : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private TMP_Text _inspectionDateField;
    [SerializeField] private CheckButton _systemsCheck;
    [SerializeField] private CheckButton _electronicsCheck;
    [SerializeField] private CheckButton _identificationCheck;
    [SerializeField] private TMP_InputField _noteField;
    [SerializeField] private Transform _container;
    [SerializeField] private AnalyticReport _template;
    [SerializeField] private Button _addButton;
    [SerializeField] private CalendarController _calendarController;
    [SerializeField] private ReportsDisplay _scrollView;

    private string _inspectionDate;

    private CheckButton.Status _systemsStatus;
    private CheckButton.Status _electronicsStatus;
    private CheckButton.Status _identificationStatus;

    private void Awake()
    {
        _calendarController.OnChoosedDate += TryUnblockAddButton;
        _calendarController.OnGiveSmashDate += SetInspectionDate;
        _systemsCheck.OnCheckButtonClick += ChangeSystemStatus;
        _electronicsCheck.OnCheckButtonClick += ChangeElectronicStatus;
        _identificationCheck.OnCheckButtonClick += ChangeIdentificationStatus;
    }

    private void Start()
    {
        _addButton.onClick.AddListener(AddAnalytic);

        TryUnblockAddButton();
    }

    private void OnDisable()
    {       
        _nameField.text = "";
        _inspectionDateField.text = "";
        _noteField.text = "";
        _addButton.interactable = false;
    }

    private void OnDestroy()
    {
        _calendarController.OnChoosedDate -= TryUnblockAddButton;
        _calendarController.OnGiveSmashDate -= SetInspectionDate;
        _systemsCheck.OnCheckButtonClick -= ChangeSystemStatus;
        _electronicsCheck.OnCheckButtonClick -= ChangeElectronicStatus;
        _identificationCheck.OnCheckButtonClick -= ChangeIdentificationStatus;
    }

    public void OpenInfo(string name)
    {
        _nameField.text = name;

        gameObject.GetComponent<PanelAnimation>().OpenRequest();
    }

    private void SetInspectionDate(string date, string month, string year)
    {
        _inspectionDate = $"{date}.{month}.{year}";
    }

    private void AddAnalytic()
    {
        AnalyticReport analytic = Instantiate(_template, _container);
        analytic.Render(_nameField.text, _inspectionDate, _inspectionDateField.text, _noteField.text, _systemsStatus, _electronicsStatus, _identificationStatus);

        _scrollView.TryChangeScrollVisible();;
        gameObject.GetComponent<PanelAnimation>().CloseRequest();
    }

    // Блокируем кнопку Add пока пользователь не прожмет все поля
    private void TryUnblockAddButton()
    {
        if (_inspectionDateField.text == "")
        {
            _addButton.interactable = false;
            return;
        }

        if (!_systemsCheck.IsChoosed)
        {
            _addButton.interactable = false;
            return;
        }

        if (!_electronicsCheck.IsChoosed)
        {
            _addButton.interactable = false;
            return;
        }

        if (!_identificationCheck.IsChoosed)
        {
            _addButton.interactable = false;
            return;
        }

        _addButton.interactable = true;
    }

    // Обновляем статусы при нажатии на Good/Violated

    private void ChangeSystemStatus(CheckButton.Status status)
    {
        _systemsStatus = status;
        TryUnblockAddButton();
    }

    private void ChangeElectronicStatus(CheckButton.Status status)
    {
        _electronicsStatus = status;
        TryUnblockAddButton();
    }

    private void ChangeIdentificationStatus(CheckButton.Status status)
    {
        _identificationStatus = status;
        TryUnblockAddButton();
    }
}
