using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PopupDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] static TextMeshProUGUI txt;
    [SerializeField] Vector2 font_size_range;
    [SerializeField] float popup_speed;
    Color32 default_color = new(255, 255, 225, 255), alert_color_friendly = new(15, 129, 32, 255), alert_color_enemy = new(158, 12, 26, 255);
    DifficultyManager manager;

    //green 240, 255, 225
    //red  255, 240, 225

    //cyan 51, 204, 158
    //magenta 204, 51, 97


    public delegate void PopupEvent();
    public event PopupEvent OnPopupFinish;


    public bool CanPopup { get; private set; }





    void Start()
    {


        CanPopup = true;

        manager = FindObjectOfType<DifficultyManager>();
        //240, 255, 225
        GetComponent<RectTransform>().anchoredPosition = new Vector3(-122, -80, 0);
        txt = GetComponent<TextMeshProUGUI>();
        txt.color = default_color;
        txt.text = "";



        manager.OnDifficultyValueChange += Popup;





    }

    // Update is called once per frame
    void Update()
    {

    }


    void Popup(DifficultyEventArgs e)
    {
        Debug.Log("Popped");
        IEnumerator popup()
        {

            txt.fontSize = font_size_range.x;
            while (txt.fontSize < font_size_range.y)
            {

                txt.fontSize += popup_speed;

                yield return null;
            }
            txt.fontSize = font_size_range.y;


            Color alert_color = (e.Affected == AffectedTarget.ENEMY) ? alert_color_enemy : alert_color_friendly;
            for (int i = 0; i < 9; i++)
            {
                txt.color = (i % 2 == 0) ? alert_color : default_color;
                Debug.Log(txt.color);
                yield return new WaitForSeconds(0.15f);
            }


            while (txt.fontSize > font_size_range.x)
            {

                txt.fontSize -= popup_speed;

                yield return null;
            }

            txt.fontSize = font_size_range.x;


            CanPopup = true;
            OnPopupFinish?.Invoke();









        }
        CanPopup = false;
        txt.text = e.Message;
        StartCoroutine(popup());

    }


    







}








