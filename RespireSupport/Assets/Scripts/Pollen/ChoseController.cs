using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoseController : MonoBehaviour
{
    [SerializeField] private List<Button> btnsList;
    [SerializeField] private Sprite notActiveBtn;
    [SerializeField] private Sprite activeBtn;

    [SerializeField] private bool isMultiSelectEnabled = false; // Режим множественного выбора
    private List<Button> activeButtons = new List<Button>();    // Список активных кнопок

    private void Start()
    {
        foreach (var btn in btnsList)
        {
            SetButtonInactive(btn);
            btn.onClick.AddListener(() => OnButtonClick(btn));
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        if (isMultiSelectEnabled)
        {
            // В режиме множественного выбора добавляем или убираем кнопку из активных
            if (activeButtons.Contains(clickedButton))
            {
                SetButtonInactive(clickedButton);
                activeButtons.Remove(clickedButton);
            }
            else
            {
                SetButtonActive(clickedButton);
                activeButtons.Add(clickedButton);
            }
        }
        else
        {
            // В обычном режиме деактивируем предыдущую активную кнопку
            foreach (var button in activeButtons)
            {
                SetButtonInactive(button);
            }
            activeButtons.Clear();

            // Активируем текущую кнопку
            if (!activeButtons.Contains(clickedButton))
            {
                SetButtonActive(clickedButton);
                activeButtons.Add(clickedButton);
            }
        }
    }

    private void SetButtonActive(Button button)
    {
        button.image.sprite = activeBtn;
    }

    private void SetButtonInactive(Button button)
    {
        button.image.sprite = notActiveBtn;
    }
} 
