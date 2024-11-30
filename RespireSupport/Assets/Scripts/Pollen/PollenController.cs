using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PollenController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown typesOfPollen;
    [SerializeField] private string enterName = "Types of pollen";

    private void Start()
    {
        typesOfPollen.captionText.text = enterName;
    }    
}
