using UnityEngine;
using System.Collections;
using System.Linq;
using TMPro;
using System;
using System.Collections.Generic;

public class MouseReactivity : MonoBehaviour
{
    // Start is called before the first frame update









    [SerializeField] GameObject turret_head_1, turret_head_2;
    [SerializeField] GameObject turret_station_1, turret_station_2;


    [SerializeField] GameObject turret_control_1, turret_control_2;
    [SerializeField] GameObject slider_control, slider_turret_head, slider_loader_full_auto_pivot, slider_loader_bolt_pivot;


    [SerializeField] TextMeshProUGUI score_counter;






    public Material block_material;




    Dictionary<string, Action<int, RaycastHit>> tag_reaction_dict;
    void Start()
    {


        tag_reaction_dict = new Dictionary<string, Action<int, RaycastHit>>
        {
            { Tags.COLOR_COLLIDER_1, ColorColliderReaction },
            { Tags.COLOR_COLLIDER_2, ColorColliderReaction },
            { Tags.LASER_TARGET_1, LaserTargetReaction },
            { Tags.LASER_TARGET_2, LaserTargetReaction },
            { Tags.AUTO_COLLIDER_1, AutoColliderReaction },
            { Tags.AUTO_COLLIDER_2, AutoColliderReaction },
            { Tags.SLIDER_CONTROL_COLLIDER, SliderControlReaction },
            {Tags.SLIDER_FULL_AUTO_COLLIDER, SliderFullAutoReaction },
            {Tags.SLIDER_BOLT_COLLIDER, SliderBoltReaction },
            { Tags.DISRUPTOR,  EmptyReaction},
             { Tags.SPINNER,  EmptyReaction}
        };

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {




            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {

                int hit_tag_number_id = 0;
                char last_tag_char = hit.transform.tag.ToCharArray()[hit.transform.tag.ToCharArray().Length - 1];
                try
                {
                    hit_tag_number_id = int.Parse(last_tag_char.ToString());


                }
                catch (Exception e)
                {
                    e.Equals(e);


                }





                tag_reaction_dict[hit.transform.tag](hit_tag_number_id, hit);
                //  RaycastColliderChecks(hit);



            }





        }
    }





    void EmptyReaction(int i, RaycastHit hit)
    {

        Debug.Log("EmptyReact");

    }

    void SliderControlReaction(int i, RaycastHit hit)
    {
        slider_control.GetComponent<ControlColorChange>().StartChange(hit.transform.GetComponent<Renderer>().material);
        slider_control.GetComponent<SliderControlActivation>().EngageActivation();
    }



    void SliderFullAutoReaction(int i, RaycastHit hit) 
    {
        slider_control.GetComponent<SliderLoaderControlColorChange>().Engage(hit.transform.GetComponent<Renderer>().material, true, false);
        slider_loader_full_auto_pivot.GetComponent<SliderLoaderRecharge>().OnActivationInvoke(this);


        slider_control.GetComponent<SliderLoaderControlColorChange>().Engage(hit.transform.GetComponent<Renderer>().material, false, true);
        slider_loader_bolt_pivot.GetComponent<SliderLoaderRecharge>().OnDeactivationInvoke(this);


        slider_turret_head.GetComponent<SliderShooting>().loader_recharge = slider_loader_full_auto_pivot.GetComponent<SliderLoaderRecharge>();
    }

    void SliderBoltReaction(int i, RaycastHit hit)
    {
        slider_control.GetComponent<SliderLoaderControlColorChange>().Engage(hit.transform.GetComponent<Renderer>().material, false, false);
        slider_loader_bolt_pivot.GetComponent<SliderLoaderRecharge>().OnActivationInvoke(this);


        slider_control.GetComponent<SliderLoaderControlColorChange>().Engage(hit.transform.GetComponent<Renderer>().material, true, true);
        slider_loader_full_auto_pivot.GetComponent<SliderLoaderRecharge>().OnDeactivationInvoke(this);


        slider_turret_head.GetComponent<SliderShooting>().loader_recharge = slider_loader_bolt_pivot.GetComponent<SliderLoaderRecharge>();
    }


    void ColorColliderReaction(int i, RaycastHit hit)
    {


        GameObject turret_head, turret_station, turret_control;


        switch (i)
        {
            case 1:
                if (turret_control_1.GetComponent<ControlManager>().Disabled) { return; }
                turret_head = turret_head_1;
                turret_station = turret_station_1;
                turret_control = turret_control_1;
                break;
            case 2:
                if (turret_control_2.GetComponent<ControlManager>().Disabled) { return; }
                turret_head = turret_head_2;
                turret_station = turret_station_2;
                turret_control = turret_control_2;
                break;
            default: throw new ArgumentException("Input must be 1 or 2");
        }

        if (!turret_head.GetComponent<TargetBomb>().isTargeting)
        {
            turret_head.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = hit.transform
                .GetComponent<Renderer>()
                .material;

            turret_station.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = hit.transform
                .GetComponent<Renderer>()
                .material;

            turret_control
                .GetComponent<ControlColorChange>()
                .StartChange(hit.transform.GetComponent<Renderer>().material);
        }


    }

    void AutoColliderReaction(int i, RaycastHit hit)
    {
        if (turret_control_1.GetComponent<ControlManager>().Disabled) { return; }

        GameObject turret_control, turret_station_charge, turret_head;
        string tag;

        switch (i)
        {
            case 1:
                if (turret_control_1.GetComponent<ControlManager>().Disabled) { return; }


                turret_control = turret_control_1;

                tag = Tags.LASER_TARGET_1;
                turret_station_charge = turret_station_1.transform.GetChild(0).gameObject;
                turret_head = turret_head_1;
                break;
            case 2:
                if (turret_control_2.GetComponent<ControlManager>().Disabled) { return; }

                turret_control = turret_control_2;

                tag = Tags.LASER_TARGET_2;
                turret_station_charge = turret_station_2.transform.GetChild(0).gameObject;
                turret_head = turret_head_2;
                break;
            default: throw new ArgumentException("Input must be 1 or 2");
        }


        if (hit.transform.GetComponent<Renderer>().material.color == block_material.color) { return; }

        turret_control
            .GetComponent<ControlColorChange>()
            .StartChange(hit.transform.GetComponent<Renderer>().material);







        turret_head.GetComponent<TargetBomb>().EngageBarrageStart(tag, turret_station_charge);











    }

    void LaserTargetReaction(int i, RaycastHit hit)
    {


        GameObject turret_head;
        switch (i)
        {
            case 1:
                if (turret_control_1.GetComponent<ControlManager>().Disabled) { return; }
                turret_head = turret_head_1;
                break;
            case 2:
                if (turret_control_2.GetComponent<ControlManager>().Disabled) { return; }
                turret_head = turret_head_2;
                break;
            default: throw new ArgumentException("Must be 1 or 2");
        }


        GameObject turret_head_charge = turret_head.transform.GetChild(0).gameObject;
        if ((hit.transform.gameObject.GetComponent<Renderer>().sharedMaterials[1].color == turret_head_charge.GetComponent<Renderer>().sharedMaterial.color) && !turret_head.GetComponent<TargetBomb>().isTargeting
                   )
        {
            turret_head
                .GetComponent<TargetBomb>()
                .StartTargeting(hit.transform.gameObject);


            ScoreCounter.Increase(hit.transform.GetComponent<IScoreEnumerable>().ScoreReward());
            GameObject.FindGameObjectWithTag(Tags.SPINNER).GetComponent<SpinnerColorChange>().ChangeIndexHolder(0, -1);
        }

    }


}
