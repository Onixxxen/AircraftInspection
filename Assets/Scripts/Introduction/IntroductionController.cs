using System.Collections.Generic;
using UnityEngine;

public class IntroductionController : MonoBehaviour
{
    [SerializeField] private List<Introduction> _introductions;

    private int _currentStage = 0;

    public List<Introduction> Introductions => _introductions;

    private void Awake()
    {
        Init();
    }

    // Тут могли бы быть еще методы для свайпов, но я блин не успел :(

    public void Init()
    {
        for (int i = 0; i < _introductions.Count; i++)
        {
            if (i != _currentStage)
                _introductions[i].CloseStage();
            else
                _introductions[i].OpenStage();
        }

        TryBlockButtons();
    }

    public void OpenNextStage()
    {
        if (_currentStage + 1 >= _introductions.Count)
            return;

        _introductions[_currentStage].CloseStage();
        _currentStage += 1;
        _introductions[_currentStage].OpenStage();
        TryBlockButtons();
    }

    public void OpenPreviousStage()
    {
        if (_currentStage - 1 < 0)
            return;

        _introductions[_currentStage].CloseStage();
        _currentStage -= 1;
        _introductions[_currentStage].OpenStage();

        TryBlockButtons();
    }

    public void TryBlockButtons()
    {
        if (_currentStage + 1 >= _introductions.Count)
            _introductions[_currentStage].ChangeNextButtonStatus(false);
        else
            _introductions[_currentStage].ChangeNextButtonStatus(true);

        if (_currentStage - 1 < 0)
            _introductions[_currentStage].ChangeBackButtonStatus(false);
        else
            _introductions[_currentStage].ChangeBackButtonStatus(true);
    }    
}
