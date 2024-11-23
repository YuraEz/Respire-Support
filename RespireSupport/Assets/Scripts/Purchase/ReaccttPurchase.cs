using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReaccttPurchase : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            PrchsM.Instance.ReactivatePurchases();
        });
    }
}
