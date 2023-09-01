using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderLoaderControlColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Material off_mat;
    void Start()
    {
        
    }

    // Update is called once per frame





    public void Engage(Material mat,bool full_auto, bool off)
    {
        int indexToChange = (full_auto) ? 6 : 4;


        if (off) { mat = off_mat; }

        
            Material[] mats = new Material[GetComponent<Renderer>().materials.Length] ;

        for (int i = 0; i < mats.Length; i++) 
        {
            mats[i] = (i == indexToChange) ? mat : GetComponent<Renderer>().materials[i];
        }


        GetComponent<Renderer>().materials = mats;
    }
}
