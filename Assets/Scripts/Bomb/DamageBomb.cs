using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageBomb : MonoBehaviour
{
    // Start is called before the first frame update

    public float damageSpeed;
    [SerializeField] float min_scale_down_size, scale_down_increment;
    GameObject bomb;
  private  Material colorMat;

    public bool DisabledRewards { get; set; }

    void Start()
    {
        bomb = gameObject;
    }

    // Update is called once per frame
  



  public void StartDamage(){

 colorMat  = bomb.GetComponent<Renderer>().materials[1];

        StartCoroutine(ScaleDown());
        Destroy(bomb.GetComponent<BombFall>());

 //  Debug.Log(colorMat.name + " colorMat");
    StartCoroutine(ChangeMaterial());
  
  }




    IEnumerator ChangeMaterial(){


        
        int i = 0;
        while(true){



            Material[] deltaMaterials = bomb.GetComponent<Renderer>().materials;
            deltaMaterials[i] = colorMat;
          bomb.GetComponent<Renderer>().materials = deltaMaterials;
          i++;  

             yield return new WaitForSeconds(damageSpeed);
            if(i == 7){


                Destroy(bomb);
                break;
            }
           
        }
        




    }


    IEnumerator ScaleDown() 
    {

        while (bomb.transform.localScale.x > min_scale_down_size)
        {

            bomb.transform.localScale = new Vector3(bomb.transform.localScale.x - scale_down_increment, bomb.transform.localScale.y - scale_down_increment, bomb.transform.localScale.z - scale_down_increment);
            yield return null;
        }
    }

   
}



