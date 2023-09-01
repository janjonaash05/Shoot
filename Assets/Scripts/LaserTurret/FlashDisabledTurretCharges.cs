using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDisabledTurretCharges : MonoBehaviour
{


    [SerializeField] Material[] mats;
    [SerializeField] float delay;
    [SerializeField] GameObject turret_head_charge, turret_station_charge;
    [SerializeField] Material off_mat;
    void Start()
    {
        GetComponent<ControlManager>().OnDisabled += StartFlashing;
        GetComponent<ControlManager>().OnEnabled += EndFlashing;
    }




    
    void StartFlashing(object sender, EventArgs e)
    {
       
        StartCoroutine(Flash());
    }


    void EndFlashing(object sender, EventArgs e)
    {
        turret_head_charge.GetComponent<Renderer>().material = off_mat;
        turret_station_charge.GetComponent<Renderer>().material = off_mat;
        StopAllCoroutines();
    }




    IEnumerator Flash()
    {
        while (true)
        {
            foreach (Material m in mats)
            {
                turret_head_charge.GetComponent<Renderer>().material = m;
                turret_station_charge.GetComponent<Renderer>().material = m;

                yield return new WaitForSeconds(delay);
            }


        }


    }





}
