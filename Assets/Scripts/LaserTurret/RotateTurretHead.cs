using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurretHead : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject turret_charge_head;
   public Vector3 rotation;
   public float charge_speed_multiplier;
   public float ShootModeMultiplier;
    bool lockedOn = false;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(!lockedOn){
          transform.Rotate(rotation * Time.deltaTime);
          turret_charge_head.transform.Rotate(-rotation * charge_speed_multiplier* Time.deltaTime);
        }

    
    }




    public void EngageShootMode(){

      rotation = rotation * ShootModeMultiplier;

    }

    public void DisengageShootMode(){


      rotation = rotation / ShootModeMultiplier;
    }





    
}
