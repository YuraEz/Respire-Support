using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
//using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    public GameObject dayPrefab; // ������ ��� ���
    public Transform gridParent; // Grid Layout Group ��� ����
    public TextMeshProUGUI monthYearText; // ����� � ������� ����� ���������
    public Button nextMonthButton, prevMonthButton; // ������ ��� ������������ �������
    public Button nextYearButton, prevYearButton; // ������ ��� ������������ �����

    public GameObject Panel;
    public Button backBtn;

    private DateTime currentDate; // ������� ����
    private List<GameObject> dayObjects = new List<GameObject>(); // ������ ��� �������� ��������� ����

    public NoteLoader NoteContainer;


    public bool isTr;

    private void Start()
    {
        if (Panel) Panel.SetActive(false);

        if (backBtn) backBtn.onClick.AddListener(() =>
        {
            NoteContainer.SpawnNotes(PlayerPrefs.GetInt("noteId", 0));
          Panel.SetActive(false);
          
        });

        currentDate = DateTime.Now; // ������������� ������� ����
        UpdateCalendar(); // ��������� ���������

        // ����������� ������
        nextMonthButton.onClick.AddListener(() => ChangeMonth(1));
        prevMonthButton.onClick.AddListener(() => ChangeMonth(-1));
        if (nextYearButton) nextYearButton.onClick.AddListener(() => ChangeYear(1));
        if (prevYearButton) prevYearButton.onClick.AddListener(() => ChangeYear(-1));
    }

    // ��������� ���������
    private void UpdateCalendar()
    {
        // ������� ������ ���
        foreach (var day in dayObjects)
        {
            Destroy(day);
        }
        dayObjects.Clear();

        // ������������� �������� ������ � ���� � ������� "/ Month Year"
        monthYearText.text = $"{currentDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture)}";

        // �������� ���� ������� ��� ������
        DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

        // ����������, �� ����� ���� ������ ���������� ������ ���� ������
        int startDay = (int)firstDayOfMonth.DayOfWeek;
        if (startDay == 0) startDay = 7; // ���������� ����������� � ����� ������

        // �������� ���������� ���� � ������
        int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

        // ������� ������ ������ ����� ������ ���� ������
        for (int i = 1; i < startDay; i++)
        {
            CreateEmptyDay();
        }

        // ������� ��� ������
        for (int day = 1; day <= daysInMonth; day++)
        {
            CreateDay(day);
        }
    }

    // ������� ������ ����
    private void CreateEmptyDay()
    {
        GameObject emptyDay = Instantiate(dayPrefab, gridParent);
        emptyDay.GetComponentInChildren<TextMeshProUGUI>().text = ""; // ������ �����
        
        dayObjects.Add(emptyDay);
    }

    // ������� ���� � �������
    private void CreateDay(int day)
    {
        GameObject dayObj = Instantiate(dayPrefab, gridParent);
        TextMeshProUGUI dayText = dayObj.GetComponentInChildren<TextMeshProUGUI>();
        dayText.gameObject.SetActive(true);
        dayText.text = day.ToString(); // ������������� ����� ���

        Button dayButton = dayObj.GetComponent<Button>();
        DateTime dayDate = new DateTime(currentDate.Year, currentDate.Month, day);
        dayButton.onClick.AddListener(() => OnDayClick(dayDate, dayButton)); // ��������� ���������� �����

        dayObjects.Add(dayObj);
    }

    // ������� ��� ����� �� ����
    public TextMeshProUGUI tx;

    private void OnDayClick(DateTime dayDate, Button self)
    {


        if (Panel) Panel.SetActive(true);

        Debug.Log(dayDate.ToString("MMMM d, yyyy"));

        Debug.Log("������� ����: " + dayDate.ToString("dd.MM.yyyy"));

        // �������� ������ �������� ������ � ����������� Image
        Transform firstChild = self.transform.GetChild(0);
        if (firstChild)
        {
            // ���������, ���� �� � ������� ������� ��������� Image � ���������� ���
            Image childImage = firstChild.GetComponent<Image>();
            if (childImage)
            {
                if (!Panel) childImage.gameObject.SetActive(true); // ���������� ������
            }
            else
            {
                Debug.LogWarning("Image �� ������ � ������� ��������� �������.");
            }

            // ���� �������� ������ � ������� �������, ������� �������� TextMeshProUGUI
            Transform textChild = firstChild.GetChild(0);
            if (textChild)
            {
                TextMeshProUGUI childText = textChild.GetComponent<TextMeshProUGUI>();
                if (childText)
                {
                    if (!Panel) childText.text = dayDate.Day.ToString(); // ������������� ������� ����
                }
                else
                {
                    Debug.LogWarning("TextMeshProUGUI �� ������ � ��������� �������.");
                }
            }
            else
            {
                Debug.LogWarning("� ������� ������� ������ ��� ��������� �������.");
            }
        }
        else
        {
            Debug.LogWarning("������ �� ����� �������� ��������.");
        }

        // ������� ��������� ���� � ������� "June 7, 2024"
        if (self.GetComponentInChildren<TextMeshProUGUI>() is TextMeshProUGUI selectedDateText)
        {
           if (tx)  tx.text = dayDate.ToString("MMMM d, yyyy");
            // selectedDateText.text = "�� �������: " + dayDate.ToString("MMMM d, yyyy");
        }

        Debug.Log("������� ����: " + dayDate.ToString("dd.MM.yyyy"));
        // ����� ����� ������� ����� ��� �������� �������
    }

    // �������� �����
    private void ChangeMonth(int delta)
    {
        currentDate = currentDate.AddMonths(delta);
        UpdateCalendar();
    }

    // �������� ���
    private void ChangeYear(int delta)
    {
        currentDate = currentDate.AddYears(delta);
        UpdateCalendar();
    }
}
