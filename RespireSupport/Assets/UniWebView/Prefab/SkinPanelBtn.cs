using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPanelBtn : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    [SerializeField] private bool turn;
    [SerializeField] private Button setbtn;

    private void Start()
    {
        setbtn.onClick.AddListener(() =>{
            foreach (GameObject obj in objects)
            {
                obj.SetActive(turn);
            }

        });
    }
}
