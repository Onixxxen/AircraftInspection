using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AirplaneEdit : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private TMP_InputField _modelField;
    [SerializeField] private TMP_InputField _serialNubmerField;
    [SerializeField] private TMP_Text _lastInspectionField;
    [SerializeField] private TMP_Text _upcomingInspectionField;
    [SerializeField] private AirplaneInfo _airplaneInfo;
    [SerializeField] private Button _saveButton;

    private AirplaneReport _airplaneReport;

    private void Start()
    {
        _saveButton.onClick.AddListener(SaveInfo);
    }

    private void OnDisable()
    {
        _nameField.text = "";
        _modelField.text = "";
        _serialNubmerField.text = "";
        _lastInspectionField.text = "";
        _upcomingInspectionField.text = "";
    }

    public void OpenEdit(AirplaneReport airplane, string name, string model, string serialNumber, string lastInspection, string upcomingInspection)
    {
        _airplaneReport = airplane;

        _nameField.text = name;
        _modelField.text = model;
        _serialNubmerField.text = serialNumber;
        _lastInspectionField.text = lastInspection;
        _upcomingInspectionField.text = upcomingInspection;

        gameObject.gameObject.GetComponent<PanelAnimation>().OpenRequest();
    }

    private void SaveInfo()
    {
        _airplaneReport.Render(_nameField.text, _modelField.text, _serialNubmerField.text, _lastInspectionField.text, _upcomingInspectionField.text);
        _airplaneInfo.GetComponent<PanelAnimation>().CloseRequest();
        gameObject.GetComponent<PanelAnimation>().CloseRequest();
    }
}
