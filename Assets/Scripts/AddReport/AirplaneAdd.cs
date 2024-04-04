using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AirplaneAdd : ReportAdd
{
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_InputField _modelField;
    [SerializeField] private TMP_InputField _serialNubmerField;
    [SerializeField] private TMP_Text _lastInspectionField;
    [SerializeField] private TMP_Text _upcomingInspectionField;
    [SerializeField] private Transform _container;
    [SerializeField] private AirplaneReport _template;
    [SerializeField] private Button _addButton;
    [SerializeField] private ReportsDisplay _scrollView;

    private void Awake()
    {
        _addButton.onClick.AddListener(AddAirplane);
    }

    private void OnDisable()
    {
        _nameField.text = "";
        _modelField.text = "";
        _serialNubmerField.text = "";
        _lastInspectionField.text = "";
        _upcomingInspectionField.text = "";        
    }

    private void AddAirplane()
    {
        AirplaneReport airplane = Instantiate(_template, _container);
        airplane.Render(_nameField.text, _modelField.text, _serialNubmerField.text, _lastInspectionField.text, _upcomingInspectionField.text);

        _scrollView.TryChangeScrollVisible();
        gameObject.GetComponent<PanelAnimation>().CloseRequest();
    }
}
