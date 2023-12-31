using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class DestroyDisruptor : MonoBehaviour
{
    // Start is called before the first frame update


    Dictionary<int, int> index_order_dict;



    [SerializeField] float color_change_delay;
    [SerializeField] Material white;

    void Start()
    {
        index_order_dict = new Dictionary<int, int>() { { 1, 8 }, { 2, 7 }, { 3, 6 }, { 4, 5 }, { 5, 4 }, { 6, 3 }, { 7, 2 }, { 8, 9 }, { 9, 1 }, { 10, 0 } };
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Engage()
    {

        

        Vector3 target = Camera.main.transform.position;
      
        GetComponent<RotateDisruptor>().EngageRotation(target);
        Destroy(GetComponent<RotateDisruptor>());
        ScoreCounter.Increase(GetComponent<IScoreEnumerable>().ScoreReward());
        GetComponent<IScoreEnumerable>().DisabledRewards = true;

        ColorChange();
        GetComponent<DisruptorStartEndMovement>().CancelMovingUp();
        GetComponent<DisruptorStartEndMovement>().MoveDown();
        Destroy(  GetComponent<DisruptorMovement>());
        Destroy(GetComponent<DisruptorColorChange>());

        

        
    }



  


    void ColorChange()
    {


        GetComponent<Renderer>().materials = new Material[] { white, white, white, white, white, white, white, white, white, white };


    }


}
