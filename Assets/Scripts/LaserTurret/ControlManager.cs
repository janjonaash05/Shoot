using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class ControlManager : MonoBehaviour
{
    // Start is called before the first frame update



    public event EventHandler OnDisabled, OnEnabled;


    public bool Disabled;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }




   public async void DisableFor(float s) 
    {

        OnDisabled?.Invoke(this, EventArgs.Empty);
        Disabled = true;
        await Task.Delay((int)(s * 1000));

        OnEnabled?.Invoke(this, EventArgs.Empty);
        Disabled = false;


    }

   



}
