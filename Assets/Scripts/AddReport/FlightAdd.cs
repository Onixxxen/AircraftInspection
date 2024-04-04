using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlightAdd : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_Text _inspectionDateField;
    [SerializeField] private CalendarController _calendarController;
    [SerializeField] private Transform _container;
    [SerializeField] private FlightReport _template;
    [SerializeField] private Button _addButton;
    [SerializeField] private ReportsDisplay _scrollView;

    private string _inspectionDate;

    private void Awake()
    {
        _calendarController.OnGiveSmashDate += SetInspectionDate;
    }

    private void Start()
    {
        _addButton.onClick.AddListener(AddFlight);
    }

    private void OnDisable()
    {       
        _nameField.text = "";
        _inspectionDate = "";
        _inspectionDateField.text = "";
    }

    private void OnDestroy()
    {
        _calendarController.OnGiveSmashDate -= SetInspectionDate;
    }

    private void AddFlight()
    {
        FlightReport flight = Instantiate(_template, _container);
        flight.Render(_nameField.text, _inspectionDate, _inspectionDateField.text);

        _scrollView.TryChangeScrollVisible();
        gameObject.GetComponent<PanelAnimation>().CloseRequest();
    }

    private void SetInspectionDate(string date, string month, string year)
    {
        _inspectionDate = $"{date}.{month}.{year}";
    }
}
