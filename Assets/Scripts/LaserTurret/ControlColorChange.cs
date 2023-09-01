using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlColorChange : MonoBehaviour
{
    public float waitTime;
    [SerializeField] float darkeningIntensity;
    [SerializeField] Dictionary<string, int> mat_index_dict;
    private Material[] mats;
    void Start()
    {

        mats = GetComponent<Renderer>().materials;

        mat_index_dict = new Dictionary<string, int>();


        for (int i = 0; i < mats.Length; i++)
        {

            try { mat_index_dict.Add(mats[i].name, i); } catch(Exception) { };

        }
    }






    public void StartChange(Material mat)
    {
        // returnMat = mat;
        //  mat.name = mat.name.Replace(" (Instance)","");

        mats = GetComponent<Renderer>().materials;
        int index = mat_index_dict[mat.name];
        StartCoroutine(Change(index));

    }

    IEnumerator Change(int index)
    {


        Color old = mats[index].GetColor("_EmissionColor");

        mats[index].SetColor("_EmissionColor", mats[index].color * darkeningIntensity);

        GetComponent<Renderer>().materials = mats;




        yield return new WaitForSeconds(waitTime);





        mats[index].SetColor("_EmissionColor", old);
        GetComponent<Renderer>().materials = mats;




    }
}
