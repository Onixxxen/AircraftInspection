using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalendarController : MonoBehaviour
{
    [SerializeField] private GameObject _calendarPanel;
    [SerializeField] private TMP_Text _yearNumText;
    [SerializeField] private TMP_Text _monthNumText;
    [SerializeField] private GameObject _item;
    [SerializeField] private bool _isFullMonthStyle;
    [SerializeField] private bool _isSmash;
    
    private DateTime _dateTime;  
    private string _monthText;
    private bool _isInitialized;     

    private const int _totalDateNum = 42;

    public List<GameObject> DateItems = new List<GameObject>();
    public static CalendarController _calendarInstance;

    public event Action OnChoosedDate;
    public event Action<string, string, string> OnGiveSmashDate;   

    private void Start()
    {
        _calendarInstance = this;
    }

    private void Initialize()
    {
        Vector3 startPos = _item.transform.localPosition;
        DateItems.Clear();
        DateItems.Add(_item);

        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = Instantiate(_item);
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(_item.transform.parent);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 31 + startPos.x, startPos.y - (i / 7) * 25, startPos.z);

            DateItems.Add(item);
        }

        _dateTime = DateTime.Now;

        CreateCalendar();

        _isInitialized = true;
    }

    private void CreateCalendar()
    {
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);

        int date = 0;
        for (int i = 0; i < _totalDateNum; i++)
        {
            TMP_Text label = DateItems[i].GetComponentInChildren<TMP_Text>();
            DateItems[i].SetActive(false);

            if (i >= index)
            {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    DateItems[i].SetActive(true);

                    label.text = (date + 1).ToString();
                    date++;
                }
            }
        }
        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = GetMonthName(_dateTime.Month);

        if (_isFullMonthStyle)
            _monthText = GetMonthName(_dateTime.Month);
        else
            _monthText = GetMonthNumber(_dateTime.Month);
    }

    private int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 0;
            case DayOfWeek.Tuesday: return 1;
            case DayOfWeek.Wednesday: return 2;
            case DayOfWeek.Thursday: return 3;
            case DayOfWeek.Friday: return 4;
            case DayOfWeek.Saturday: return 5;
            case DayOfWeek.Sunday: return 6;
        }

        return 0;
    }
    public void YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    private string GetMonthName(int month)
    {
        switch (month)
        {
            case 1: return "Jan";
            case 2: return "Feb";
            case 3: return "Mar";
            case 4: return "Apr";
            case 5: return "May";
            case 6: return "Jun";
            case 7: return "Jul";
            case 8: return "Aug";
            case 9: return "Sep";
            case 10: return "Oct";
            case 11: return "Nov";
            case 12: return "Dec";
        }

        return "";
    }

    private string GetMonthNumber(int month)
    {
        switch (month)
        {
            case 1: return "01";
            case 2: return "02";
            case 3: return "03";
            case 4: return "04";
            case 5: return "05";
            case 6: return "06";
            case 7: return "07";
            case 8: return "08";
            case 9: return "09";
            case 10: return "10";
            case 11: return "11";
            case 12: return "12";
        }

        return "";
    }

    public void ShowCalendar(TMP_Text target)
    {
        if (!_isInitialized)
        {
            Initialize();
        }
        _calendarPanel.SetActive(true);
        _calendarPanel.GetComponent<PanelAnimation>().OpenRequest();
        _target = target;

        // Возможные манипуляции со стартовой позицией календаря
        //_calendarPanel.transform.position = new Vector3(965, 475, 0);
        //Input.mousePosition-new Vector3(0,120,0);
    }

    TMP_Text _target;
    public void OnDateItemClick(string day)
    {
        if (_isFullMonthStyle)
            _target.text = day + " " + _monthText + " " + _yearNumText.text;
        else
            _target.text = day + "." + _monthText + "." + _yearNumText.text;

        if (_isSmash)
            OnGiveSmashDate?.Invoke(day, GetMonthNumber(_dateTime.Month), _yearNumText.text[^2..]);

        OnChoosedDate?.Invoke();

        _calendarPanel.GetComponent<PanelAnimation>().CloseRequest();
    }
}
