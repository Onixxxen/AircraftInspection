using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.PackageManager.UI;
using TMPro;

public class PanelAnimation : MonoBehaviour
{
    [SerializeField] private List<PanelAnimation> _windowsForClose;
    [SerializeField] private Image _currentWindow;
    [SerializeField] private Image _panel;
    [SerializeField] private Image _buttonLogo;
    [SerializeField] private TMP_Text _buttonText;
    [SerializeField] private bool _isScaleAnimation;
    [SerializeField] private Color _panelVisibleColor;
    [SerializeField] private Color _panelInvisibleColor;

    private Vector3 _maxScale;
    private Vector3 _baseScale;
    private Vector3 _minScale;

    private void Start()
    {
        if (!_isScaleAnimation)
            return;

        _maxScale = new Vector3(1.1f, 1.1f, 1.1f);
        _baseScale = new Vector3(1, 1, 1);
        _minScale = new Vector3(0, 0, 0);
    }

    private void OnDisable()
    {
        _currentWindow.gameObject.SetActive(false);

        if (_isScaleAnimation)
            _currentWindow.transform.localScale = _minScale;

        if (_panel != null)
        {
            _panel.color = _panelInvisibleColor;
            _panel.gameObject.SetActive(false);
        }
    }

    public void OpenRequest()
    {
        if (_panel != null)
            _panel.color = _panelInvisibleColor;

        if (_isScaleAnimation)
            _currentWindow.transform.localScale = _minScale;

        if (_buttonLogo != null)
            OpenButtonAnimation();

        for (int i = 0; i < _windowsForClose.Count; i++)
        {
            if (_windowsForClose[i].gameObject.activeInHierarchy)
            {
                _windowsForClose[i].CloseRequest();
                _windowsForClose[i].ClosButtonAnimation();
            }
        }

        _currentWindow.gameObject.SetActive(true);

        if (_panel != null)
            _panel.gameObject.SetActive(true);

        if (_panel != null)
            _panel.DOColor(_panelVisibleColor, 0.4f);

        if (!_isScaleAnimation)
            _currentWindow.GetComponent<Animator>().SetTrigger("Open");
        else
            StartCoroutine(OpenScaleAnimation());
    }

    public void CloseRequest()
    {
        if (!_isScaleAnimation)
            StartCoroutine(CloseUnityAnimation());
        else
            StartCoroutine(CloseScaleAnimation());
    }

    private IEnumerator OpenScaleAnimation()
    {
        _currentWindow.transform.DOScale(_maxScale, 0.3f);
        _panel.DOColor(_panelVisibleColor, 0.4f);

        yield return new WaitUntil(() => _currentWindow.transform.localScale == _maxScale);

        _currentWindow.transform.DOScale(_baseScale, 0.4f);
    }

    private IEnumerator CloseUnityAnimation()
    {
        _currentWindow.GetComponent<Animator>().SetTrigger("Close");

        yield return new WaitUntil(() => _currentWindow.GetComponent<CanvasGroup>().alpha == 0);

        _currentWindow.gameObject.SetActive(false);
    }

    private IEnumerator CloseScaleAnimation()
    {
        _currentWindow.transform.DOScale(_maxScale, 0.3f);

        yield return new WaitUntil(() => _currentWindow.transform.localScale == _maxScale);

        _currentWindow.transform.DOScale(_minScale, 0.4f);
        _panel.DOColor(_panelInvisibleColor, 0.4f);

        yield return new WaitUntil(() => _panel.color == _panelInvisibleColor);

        gameObject.SetActive(false);
    }

    private void OpenButtonAnimation()
    {
        _buttonLogo.DOColor(new Color32(186, 7, 7, 255), 0.5f);
        _buttonText.DOColor(new Color32(186, 7, 7, 255), 0.5f);
    }

    private void ClosButtonAnimation()
    {
        _buttonLogo.DOColor(new Color32(96, 96, 96, 255), 0.7f);
        _buttonText.DOColor(new Color32(255, 255, 255, 79), 0.7f);
    }
}
