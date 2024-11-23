using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }


    [SerializeField] private string startScreen;
    [SerializeField] private UICanvas[] screens;

    [Space]
    private UICanvas curScreen;

    private void Awake()
    {
        Instance = this;

        foreach (UICanvas screen in screens)
        {
            screen.Init();
        }

        ChangeScreen(startScreen);
    }

    private void Update()
    {
        if (curScreen)
        {
            curScreen.UpdateScreen();

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                Cursor.lockState = curScreen.CursorLockMode;
            }
        }
    }

    private UICanvas GetScreen(string screenName)
    {
        foreach (UICanvas screen in screens)
        {
            if (screen.ScreenName == screenName) return screen;
        }

        throw new System.Exception($"{screenName} экрана не существует!");
    }

    public void ChangeScreen(string screenName)
    {
        UICanvas nextScreen = GetScreen(screenName);

        if (curScreen && nextScreen != curScreen)
        {
            curScreen.HideScreen();
        }

        curScreen = nextScreen;
        curScreen.ShowScreen();
        Cursor.lockState = curScreen.CursorLockMode;
    }
}
