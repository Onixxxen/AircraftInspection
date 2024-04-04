using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private Slider _loadSlider;
    [SerializeField] private TMP_Text _percentText;
    [SerializeField] private GameObject _nextScreen;

    private Coroutine _updatePercentCoroutine;

    private void Awake()
    {
        gameObject.SetActive(true);
        StartCoroutine(Load());
    }

    // Загрузка-обманка. В будущем, если придется подгружать много файлов, то сюда можно запихнуть как раз уже нормальную загрузку
    private IEnumerator Load()
    {
        _loadSlider.value = 0;

        _updatePercentCoroutine = StartCoroutine(UpdatePercentText());

        _loadSlider.DOValue(79, 3);

        yield return new WaitUntil(() => _loadSlider.value == 79);
        yield return new WaitForSeconds(2);

        _loadSlider.DOValue(100, 0.5f);

        yield return new WaitUntil(() => _loadSlider.value == 100);

        StopCoroutine(_updatePercentCoroutine);
        StartApplication();
    }

    private IEnumerator UpdatePercentText()
    {
        while (true)
        {
            _percentText.text = $"{(int)_loadSlider.value}%";
            yield return new WaitForSeconds(0.2f);
        }
    }

    // Запускаем экран, который будет идти после загрузки
    private void StartApplication()
    {
        _nextScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
