using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPremiumChanger : MonoBehaviour
{
    [SerializeField] private string screenName;
    [SerializeField] private string premScreenName;

    [SerializeField] private List<Animator> onDisableEffects;


    [SerializeField] private float delay;

    private Button button;
    private UI uiManager;
    private bool isClicked;

    private void Start()
    {
        uiManager = UI.Instance;
        button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            if (isClicked) return;

            isClicked = true;

            if (onDisableEffects.Count > 0) foreach (Animator anim in onDisableEffects)
                {
                    anim.SetTrigger("change");
                }

            Invoke(nameof(ChangeScreen), delay);
        });
    }

    private void ChangeScreen()
    {
        if (PlayerPrefs.GetInt("premium", 0) == 1) uiManager.ChangeScreen(screenName);
        else uiManager.ChangeScreen(premScreenName);

        isClicked = false;
    }

}
