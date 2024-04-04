using UnityEngine;
using UnityEngine.UI;

public class Introduction : MonoBehaviour
{
    [Header("Introduction data")]
    [SerializeField] private Image _slideImage;
    [SerializeField] private Sprite _activeSlideSprite;
    [SerializeField] private Sprite _deactiveSlideSprite;
    [SerializeField] private Button _nextBeginButton;
    [SerializeField] private CanvasGroup _introductionPanel;

    [Header("Introduction setting")]
    [SerializeField] private Button _nextIntroductionButton;
    [SerializeField] private Button _previousIntroductionButton;

    public void OpenStage()
    {
        gameObject.SetActive(true);
        _introductionPanel.gameObject.SetActive(true);
        _nextBeginButton.gameObject.SetActive(true);

        _introductionPanel.alpha = Mathf.Lerp(0, 1, 2);
        _slideImage.sprite = _activeSlideSprite;
        _slideImage.GetComponent<Animator>().SetTrigger("OpenSlide");
    }

    public void CloseStage()
    {
        _slideImage.GetComponent<Animator>().SetTrigger("CloseSlide");
        _slideImage.sprite = _deactiveSlideSprite;
        _introductionPanel.alpha = Mathf.Lerp(1, 0, 2);

        _introductionPanel.gameObject.SetActive(false);
        _nextBeginButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ChangeNextButtonStatus(bool isActive)
    {
        _nextIntroductionButton.gameObject.SetActive(isActive);
    }

    public void ChangeBackButtonStatus(bool isActive)
    {
        _previousIntroductionButton.gameObject.SetActive(isActive);
    }
}
