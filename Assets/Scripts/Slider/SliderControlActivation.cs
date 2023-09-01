using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderControlActivation : MonoBehaviour
{


    public bool active = false;
    float distance;


    public const float covX = 45, covY = 45;



    public Material on_material, off_material;
    Rigidbody body;


    public GameObject slider_pivot;




    void Start()
    {





        //   originZ = slider_head.transform.position.z;
        distance = 1000;// Vector3.Distance(slider_pivot.transform.position, Camera.main.transform.position);


        //  slider_y = slider_head.transform.position.y;
        //  slider_x = slider_head.transform.position.x;
        Debug.Log("Start Body  " + body);
    }

    void Update()
    {

        if (!active)
        {


            return;
        }






        Vector3 camPos = Camera.main.transform.position + Camera.main.transform.forward * distance;



        Vector3 rotationDirection = (camPos - slider_pivot.transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(rotationDirection);
        slider_pivot.transform.rotation = rot;


        float yRot = slider_pivot.transform.rotation.eulerAngles.y + 90; // rotate turret front to actually be infront
                                                                         //Debug.Log(slider_pivot.transform.rotation.eulerAngles.x);




        float zRot = slider_pivot.transform.rotation.eulerAngles.x; //z for final quat, x is because cam rot doesnt work and had to replace z with x, y is for vertical lock
       

        zRot = zRot switch
        {

            < (360 - covY - 180) => 359,

            < 360 - covY => 360 - covY, //+1,
            _ => zRot

        };


        yRot = yRot switch
        {

            < (360 - covX) => 360 - covX,

            > 360 + covX => 360 + covX,
            _ => yRot

        };


        slider_pivot.transform.rotation = Quaternion.Euler(0, yRot, zRot);
        
      


    }




    public void EngageActivation()
    {

        active = !active;

        if(!active) { slider_pivot.GetComponentInChildren<SliderShooting>().CancelMagazine(); }

        slider_pivot.transform.GetChild(1).GetComponent<Renderer>().material = (active) ? on_material : off_material;

    }













}
