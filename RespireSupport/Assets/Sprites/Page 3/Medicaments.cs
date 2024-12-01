using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medicaments : MonoBehaviour
{
    public Button startBtn;
    public Button changebtn;
    public Button endBtn;

    public GameObject panel1;
    public GameObject panel2;


    private void Start()
    {
        panel1.SetActive(false);
        panel2.SetActive(false);


        startBtn.onClick.AddListener(() =>
        {
            panel1.SetActive(true);
            panel2.SetActive(false);
        });
        changebtn.onClick.AddListener(() => 
        {
           panel1.SetActive(false);
            panel2.SetActive(true);
        });
        endBtn.onClick.AddListener(() =>
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
        });

    }

}
