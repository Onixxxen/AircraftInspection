using UnityEngine;
using TMPro;

public class CalendarDateItem : MonoBehaviour 
{
    [SerializeField] private CalendarController _calendar;

    public void OnDateItemClick()
    {
        _calendar.OnDateItemClick(gameObject.GetComponentInChildren<TMP_Text>().text);
    }
}
