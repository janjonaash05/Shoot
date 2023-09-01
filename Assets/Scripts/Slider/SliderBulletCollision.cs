using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SliderBulletCollision : MonoBehaviour
{
    // Start is called before the first frame update


    public int DamagePotential;



    private void OnCollisionEnter(Collision other)
    {

      //  Destroy(other.gameObject.GetComponent<DisruptorMovement>());
      //  Destroy(other.gameObject.GetComponent<DisruptorColorChange>());

        


        other.gameObject.GetComponent<DamageDisruptor>().Damage(DamagePotential);
       
       

        Destroy(gameObject);
        
    }
}
