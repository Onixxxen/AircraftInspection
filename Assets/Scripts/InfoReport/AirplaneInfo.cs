using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AirplaneInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private TMP_Text _modelDescription;
    [SerializeField] private TMP_Text _modelField;
    [SerializeField] private TMP_Text _serialNubmerField;
    [SerializeField] private TMP_Text _lastInspectionField;
    [SerializeField] private TMP_Text _upcomingInspectionField;
    [SerializeField] private AirplaneEdit _airplaneEdit;
    [SerializeField] private Button _editButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private ReportsDisplay _scrollView;

    private AirplaneReport _airplaneReport;

    private void Start()
    {
        _editButton.onClick.AddListener(OpenEditRequest);
        _deleteButton.onClick.AddListener(DeleteAirplaneRequest);
    }

    private void OnDisable()
    {
        _nameField.text = "";
        _modelField.text = "";
        _serialNubmerField.text = "";
        _lastInspectionField.text = "";
        _upcomingInspectionField.text = "";
    }

    public void OpenInfo(AirplaneReport airplane, string name, string model, string serialNumber, string lastInspection, string upcomingInspection)
    {
        _airplaneReport = airplane;

        _nameField.text = name;
        _modelDescription.text = model;
        _modelField.text = model;
        _serialNubmerField.text = serialNumber;
        _lastInspectionField.text = lastInspection;
        _upcomingInspectionField.text = upcomingInspection;

        gameObject.GetComponent<PanelAnimation>().OpenRequest();
    }

    private void OpenEditRequest()
    {
        _airplaneEdit.OpenEdit(_airplaneReport, _nameField.text, _modelField.text, _serialNubmerField.text, _lastInspectionField.text, _upcomingInspectionField.text);
    }

    private void DeleteAirplaneRequest()
    {
        StartCoroutine(DeleteMonitoring());
    }

    private IEnumerator DeleteMonitoring()
    {
        Destroy(_airplaneReport.gameObject);

        yield return new WaitForSeconds(0.1f); // чтобы программа успела удалить отчет и не возникло никаких ошибок

        _scrollView.TryChangeScrollVisible();
        GetComponent<PanelAnimation>().CloseRequest();
    }
}
