using UnityEngine;

public class ReportsDisplay : MonoBehaviour
{
    private void OnEnable()
    {
        TryChangeScrollVisible();
    }   

    // ��������� ScrollView, ���� � ���������� ��� �������, � ��������
    public void TryChangeScrollVisible()
    {
        var objects = GetComponentsInChildren<IChangesScrollView>();

        if (objects.Length > 0)
            gameObject.gameObject.SetActive(true);
        else 
            gameObject.gameObject.SetActive(false);
    }
}
