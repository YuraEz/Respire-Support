using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private string screenName;
    [SerializeField] private CursorLockMode cursorLockMode;

    public string ScreenName { get => screenName; }
    public CursorLockMode CursorLockMode { get => cursorLockMode; }

    public void Init()
    {

        gameObject.SetActive(false);
    }

    public void UpdateScreen()
    {

    }

    public void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public void HideScreen()
    {
        gameObject.SetActive(false);
    }
}
