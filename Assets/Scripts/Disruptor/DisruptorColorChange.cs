using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisruptorColorChange : MonoBehaviour, IScoreEnumerable
{
    // Start is called before the first frame update


    [SerializeField] Material[] mats_storage;
    [SerializeField] Material white, primary, secondary;
    [SerializeField] float delay;
    [SerializeField] GameObject charge1, charge2;
    Dictionary<int, int> index_order_dict;

    void Start()
    {
       
        index_order_dict = new Dictionary<int, int>() { { 1,8}, { 2, 7 }, { 3, 6 }, { 4, 5 }, { 5, 4 }, { 6, 3 }, { 7, 2 }, { 8,9 } };

        InitialColorSetup();
        GetComponent<DisruptorStartEndMovement>().OnMoveUpFinish += StartColorChange;
    }



    void StartColorChange(object sender, EventArgs e)
    {



        StartCoroutine(ColorChange());
    }

    void InitialColorSetup() {

        Material[] mats = new Material[GetComponent<Renderer>().materials.Length];
        mats[0] = GetComponent<Renderer>().materials[0];
        mats[1] = GetComponent<Renderer>().materials[1];



        for(int i = 2; i <  mats.Length; i++)
        {

            mats[i] = white;


        }

        GetComponent<Renderer>().materials = mats;

    }


    int i = 0;

   

    IEnumerator ColorChange()
    {
        
        for ( i = 1; i < 9; i++) {

            var color_mats = new Material[10];


            color_mats[0] = primary;
            color_mats[1] = secondary;

            for (int j = 2; j < 10; j++) {
                color_mats[j] = white;
            }

            for (int k = 0; k < i; k++) {

                color_mats[index_order_dict[k+1]] = mats_storage[k];

                charge1.GetComponent<FlashDisruptorCharge>().StartChargeUpFlash(mats_storage[k]);
                charge2.GetComponent<FlashDisruptorCharge>().StartChargeUpFlash(mats_storage[k]);

            }




            GetComponent < Renderer >().materials = color_mats;



            yield return new WaitForSeconds(delay);


        
        
        
        
        }


        charge1.GetComponent<FlashDisruptorCharge>().StartFlashingAllColors(mats_storage);
        charge2.GetComponent<FlashDisruptorCharge>().StartFlashingAllColors(mats_storage);



        GetComponent<DisruptorMovement>().SetTargeting(true);
        GetComponent<DisruptorTargetTurretHeads>().InitiateRotations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ScoreReward()
    {
        if(DisabledRewards) return 0;

        return (9-i)*5;
    }


    public bool DisabledRewards { get; set; }
}
