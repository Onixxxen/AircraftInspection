using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AirplaneReport : MonoBehaviour, IChangesScrollView
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _modelText;
    [SerializeField] private List<Button> _openButtons; // кнопки, которые могут открыть этот отчет

    private string _name;
    private string _model;
    private string _serialNumber;
    private string _lastInspection;
    private string _upcomingInspection;

    private AirplaneInfo _airplaneInfo;

    private void Start()
    {
        _airplaneInfo = FindObjectOfType<AirplaneInfo>(true);

        for (int i = 0; i < _openButtons.Count; i++)
            _openButtons[i].onClick.AddListener(OpenInfoRequest);
    }

    public void Render(string name, string model, string serialNumber, string lastInspection, string upcomingInspection)
    {
        _name = name;
        _model = model;
        _serialNumber = serialNumber;
        _lastInspection = lastInspection;
        _upcomingInspection = upcomingInspection;

        _nameText.text = _name;
        _modelText.text = _model;
    }

    // Открываем информацию об отчете и передаем туда данные
    public void OpenInfoRequest()
    {
        _airplaneInfo.OpenInfo(this, _name, _model, _serialNumber, _lastInspection, _upcomingInspection);
    }
}
