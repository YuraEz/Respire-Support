using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokupkaScreen : MonoBehaviour
{
    public static PokupkaScreen Instance { get; private set; }

    [SerializeField]
    private float _closeDelay = 2f;

    [SerializeField]
    private float _longCloseDelay = 45f;

    [SerializeField]
    private GameObject _loadingTextGO, _successTextGO, _failedTextGO;

    [SerializeField]
    private GameObject _panelGO;

    private Coroutine _currentCloseWaitCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowLoading()
    {
        _panelGO.SetActive(true);
        TurnOffAllTexts();
        _loadingTextGO.SetActive(true);

        ClosePanel(_longCloseDelay);
    }
    public void ShowSuccess()
    {
        _panelGO.SetActive(true);
        TurnOffAllTexts();
        _successTextGO.SetActive(true);

        ClosePanel(_closeDelay);
       // PurchaseManager.Instance.bgInfo.Buyed = true;
    }
    public void ShowFailed()
    {
        _panelGO.SetActive(true);
        TurnOffAllTexts();
        _failedTextGO.SetActive(true);

        ClosePanel(_closeDelay);
    }

    public void ClosePanel(float delay)
    {
        if (_currentCloseWaitCoroutine != null) StopCoroutine(_currentCloseWaitCoroutine);
        _currentCloseWaitCoroutine = StartCoroutine(CloseWaiterByTime(delay));
    }

    private void ClosePanelHandle()
    {
        TurnOffAllTexts();
        _panelGO.SetActive(false);
    }

    private IEnumerator CloseWaiterByTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClosePanelHandle();
    }

    private void TurnOffAllTexts()
    {
        _loadingTextGO.SetActive(false);
        _successTextGO.SetActive(false);
        _failedTextGO.SetActive(false);
    }
}
