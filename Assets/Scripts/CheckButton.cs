using System;
using UnityEngine;
using UnityEngine.UI;

public class CheckButton : MonoBehaviour
{
    [SerializeField] private Button _goodButotn;
    [SerializeField] private Button _violatedButton;
    [SerializeField] private Transform _goodStatusPanel;
    [SerializeField] private Transform _violatedStatusPanel;

    public enum Status { Good, Violated }

    public event Action<Status> OnCheckButtonClick;
    public bool IsChoosed => _goodStatusPanel.gameObject.activeSelf || _violatedStatusPanel.gameObject.activeSelf;

    private void Awake()
    {
        _goodButotn.onClick.AddListener(() => ChangeStatus(Status.Good, _goodButotn));
        _violatedButton.onClick.AddListener(() => ChangeStatus(Status.Violated, _violatedButton));
    }

    private void OnDisable()
    {
        _goodStatusPanel.gameObject.SetActive(false);
        _violatedStatusPanel.gameObject.SetActive(false);
    }

    // Меняем текущий статус в зависимости от прожатой кнопки и передаем эту инфу 
    private void ChangeStatus(Status status, Button button)
    {
        if (button == _goodButotn)
        {
            if (_goodStatusPanel != null)
                _goodStatusPanel.gameObject.SetActive(true);

            if (_violatedStatusPanel != null)
                _violatedStatusPanel.gameObject.SetActive(false);
        }
        else
        {
            if (_violatedStatusPanel != null)
                _violatedStatusPanel.gameObject.SetActive(true);

            if (_goodStatusPanel != null)
                _goodStatusPanel.gameObject.SetActive(false);
        }


        OnCheckButtonClick?.Invoke(status);
    }
}
