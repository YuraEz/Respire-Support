using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    [Space]
    [SerializeField] private float delay;

    [Space]
    [SerializeField] private bool isRetry = false;
    [SerializeField] private bool isNextScene = false;

    [Space]
    [SerializeField] private bool isLvl = false;
    [SerializeField] private bool lvl1 = false;
    [SerializeField] private Image locked;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI2;
    [SerializeField] private Button btn1;

    private Button negowbogebwobegwe;
    private bool wljbgewbogewbgwe;



    private void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnBtnClick()
    {
        if (isRetry)
        {
            Scene herwegobgewgebwogbw = SceneManager.GetActiveScene();
            sceneName = herwegobgewgebwogbw.name;
        }
        if (isNextScene)
        {

        }

        if (wljbgewbogewbgwe) return;

        wljbgewbogewbgwe = true;
        Invoke(nameof(ChangeScene), delay);
    }

    private void Start()
    {
        for (int i = 0; i < sceneName.Length; i++)
        {
            // print(sceneName[i]);
        }

        negowbogebwobegwe = GetComponent<Button>();
        negowbogebwobegwe.onClick.AddListener(OnBtnClick);


        if (isLvl)
        {
            if (btn1) btn1.onClick.AddListener(OnBtnClick);

            print(sceneName);

            string gwenioegwbubgeowugbewouvjqvvwevgiuvweg = sceneName.Substring(sceneName.Length - 2);


            textMeshProUGUI.text = $"{gwenioegwbubgeowugbewouvjqvvwevgiuvweg}";


            int lastLevel = int.Parse(gwenioegwbubgeowugbewouvjqvvwevgiuvweg);

            if (lvl1)
            {
                locked.gameObject.SetActive(false);
                negowbogebwobegwe.interactable = true;

                return;
            }

            if (PlayerPrefs.GetInt("lvl", 0) + 1 >= lastLevel)
            {
                locked.gameObject.SetActive(false);

                negowbogebwobegwe.interactable = true;
            }
            else
            {
                locked.gameObject.SetActive(true);

                negowbogebwobegwe.interactable = false;

                ColorBlock wouegoegwbwefbw = negowbogebwobegwe.colors;

                wouegoegwbwefbw.disabledColor = wouegoegwbwefbw.normalColor;
                negowbogebwobegwe.colors = wouegoegwbwefbw;
            }
        }



    }
}
