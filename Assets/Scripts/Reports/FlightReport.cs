using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FlightReport : MonoBehaviour, IChangesScrollView
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _inspectionDateText;
    [SerializeField] private Button _startCheckingButton;

    private string _name;
    private string _inspectionDate;
    private string _fullInspectionDate;

    private FlightCheck _flightCheck;

    private void Start()
    {
        _flightCheck = FindObjectOfType<FlightCheck>(true);
        _startCheckingButton.onClick.AddListener(OpenCheckRequest);
    }

    public void Render(string name, string inspectionDate, string fullInspectionDate)
    {
        _name = name;
        _inspectionDate = inspectionDate;
        _fullInspectionDate = fullInspectionDate; 

        _nameText.text = _name;
        _inspectionDateText.text = $"Check before {_inspectionDate}";
    }

    // Открываем информацию об отчете и передаем туда данные
    public void OpenCheckRequest()
    {
        _flightCheck.OpenInfo(_name);
    }
}
