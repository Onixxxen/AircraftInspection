using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private TMP_Text _dateField;
    [SerializeField] private TMP_Text _systemField;
    [SerializeField] private TMP_Text _electronicsField;
    [SerializeField] private TMP_Text _identificationField;
    [SerializeField] private TMP_Text _notesField;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private ReportsDisplay _scrollView;

    private AnalyticReport _analyticReport;

    private void Start()
    {
        _deleteButton.onClick.AddListener(DeleteAnalyticRequest);
    }

    private void OnDisable()
    {
        _nameField.text = "";
        _dateField.text = "";
        _systemField.text = "";
        _electronicsField.text = "";
        _identificationField.text = "";
        _notesField.text = "";
    }

    public void OpenInfo(AnalyticReport analytic, string name, string fullInspectionDate, string notes, CheckButton.Status systems, CheckButton.Status electronics, CheckButton.Status identification)
    {
        _analyticReport = analytic;

        _nameField.text = name;
        _dateField.text = fullInspectionDate;
        _systemField.text = systems.ToString();
        _electronicsField.text = electronics.ToString();
        _identificationField.text = identification.ToString();
        _notesField.text = notes;

        // Если пользователь ничего не ввел в поле заметок, то просто поставить там "прочерк"
        if (_notesField.text == "")
            _notesField.text = "—";

        // Проверяем какие установлены статусы и меняем цвет текста в зависимости от них

        if (systems == CheckButton.Status.Good)
            _systemField.color = new Color32(50, 215, 74, 255);
        else
            _systemField.color = new Color32(220, 38, 38, 255);

        if (electronics == CheckButton.Status.Good)
            _electronicsField.color = new Color32(50, 215, 74, 255);
        else
            _electronicsField.color = new Color32(220, 38, 38, 255);

        if (identification == CheckButton.Status.Good)
            _identificationField.color = new Color32(50, 215, 74, 255);
        else
            _identificationField.color = new Color32(220, 38, 38, 255);

        gameObject.GetComponent<PanelAnimation>().OpenRequest();
    }

    private void DeleteAnalyticRequest()
    {
        StartCoroutine(DeleteMonitoring());
    }

    private IEnumerator DeleteMonitoring()
    {
        Destroy(_analyticReport.gameObject);

        yield return new WaitForSeconds(0.1f); // чтобы программа успела удалить отчет и не возникло никаких ошибок

        _scrollView.TryChangeScrollVisible();
        GetComponent<PanelAnimation>().CloseRequest();
    }
}
