using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreasePower : MonoBehaviour
{
    public float max_power;
    float power;
    public float power_levels;
    public float recharge_delay;
     public  GameObject turret_control_head;
   public GameObject AutoCollider;
   public Material block_material, allow_material;
    [SerializeField] float delta_size_unit;
    float max_size_y;
    float size_y;
    void Start()
    {
        max_size_y = transform.localScale.y;
        size_y = max_size_y;

        power = max_power;
    }

    // Update is called once per frame
    void Update()
    {
        
    }






    public void Decrease(){

        power -= max_power/power_levels;
        size_y -= max_size_y/power_levels;
        transform.localScale = new Vector3(transform.localScale.x,size_y,transform.localScale.z);


        if(power == 0){

            StartCoroutine(StartRecharge());
        }


    }


    IEnumerator StartRecharge(){

        yield return new WaitForSeconds(turret_control_head.GetComponent<ControlColorChange>().waitTime);
        Renderer rend = turret_control_head.GetComponent<Renderer>();


       

        Material[] mats = rend.materials;
        mats[6].SetColor("_EmissionColor", block_material.GetColor("_EmissionColor"));

        rend.materials = mats;


        AutoCollider.GetComponent<Renderer>().material = block_material;

        yield return StartCoroutine(Recharge());

        
        mats[6].SetColor("_EmissionColor", allow_material.GetColor("_EmissionColor"));
        rend.materials = mats;

         AutoCollider.GetComponent<Renderer>().material = allow_material;


        
    }



    IEnumerator Recharge(){


        while (size_y < max_size_y)
        {
           
            size_y += delta_size_unit;
            transform.localScale = new Vector3(transform.localScale.x, size_y, transform.localScale.z);
            yield return new WaitForSeconds(recharge_delay);

        }

        transform.localScale = new Vector3(transform.localScale.x, max_size_y, transform.localScale.z);
        power = max_power;





       
        


    }
}
