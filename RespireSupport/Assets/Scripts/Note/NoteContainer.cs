using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteContainer : MonoBehaviour
{
    public TextMeshProUGUI wellb;
    public TextMeshProUGUI dish;
    public TextMeshProUGUI location;

    public TextMeshProUGUI food;
    public TextMeshProUGUI plants;
    public TextMeshProUGUI animals;
  //  public TextMeshProUGUI way;

    [Space]
    //public TextMeshProUGUI wellb;
    public TextMeshProUGUI rate;
    public TextMeshProUGUI adress;
    public TextMeshProUGUI price;
    public TextMeshProUGUI desc;
    // public List<Star> stars;

    [Space]
    public int index;

    //[Space]
   //public GameObject panel;
   //public Button btn1;
   //public Button editBtn;
    //public Button delete;

    private void Start()
    {
      //  panel.SetActive(false);

      //  btn1.onClick.AddListener(() =>
      //  {
           // panel.SetActive(!panel.activeSelf);
      //  });
    }


    public void OnChangeStars(int amount)
    {
        ResetStars();
        SetStars(amount);
    }

    private void SetStars(int amount)
    {
     //   for (int i = 0; i < stars.Count; i++)
      //  {
       //     if (i < amount)
       //     {
              //  stars[i].on.gameObject.SetActive(true);
        //    }
        //    else
        //    {
          //      stars[i].on.gameObject.SetActive(false);
        //    }
        //}
    }

    private void ResetStars()
    {
     //   foreach (Star star in stars)
     //   {
     //       star.on.gameObject.SetActive(false);
     //   }
    }
}
