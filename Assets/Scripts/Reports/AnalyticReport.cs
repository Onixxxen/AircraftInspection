using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticReport : MonoBehaviour, IChangesScrollView
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _verificationDateText;
    [SerializeField] private Button _openButton;

    private string _name;
    private string _inspectionDate;
    private string _fullInspectionDate;
    private string _notes;
    private CheckButton.Status _systemsStatus;
    private CheckButton.Status _electronicsStatus;
    private CheckButton.Status _identificationStatus;

    private AnalyticInfo _analyticInfo;

    private void Start()
    {
        _analyticInfo = FindObjectOfType<AnalyticInfo>(true);
        _openButton.onClick.AddListener(OpenInfoRequest);
    }

    public void Render(string name, string inspectionDate, string fullInspectionDate, string notes, CheckButton.Status systems, CheckButton.Status electronics, CheckButton.Status identification)
    {
        _name = name;
        _inspectionDate = inspectionDate;
        _fullInspectionDate = fullInspectionDate;
        _notes = notes;
        _systemsStatus = systems;
        _electronicsStatus = electronics;
        _identificationStatus = identification;

        _nameText.text = _name;
        _verificationDateText.text = $"Report from the {_inspectionDate}";
    }

    // Открываем информацию об отчете и передаем туда данные
    public void OpenInfoRequest()
    {
        _analyticInfo.OpenInfo(this, _name, _fullInspectionDate, _notes, _systemsStatus, _electronicsStatus, _identificationStatus);
    }
}
