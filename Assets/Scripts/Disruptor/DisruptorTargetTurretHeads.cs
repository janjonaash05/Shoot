using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;


public class DisruptorTargetTurretHeads : MonoBehaviour
{

    GameObject charge1, charge2;

    Transform turretHead1;
    Transform cam;

    RotateDisruptor rotateDisruptor;

    void Start()
    {
        charge1 = transform.GetChild(0).gameObject;
        charge2 = transform.GetChild(1).gameObject;
        cam = Camera.main.transform;
        turretHead1 = GameObject.FindGameObjectWithTag("TurretHead1").transform;
        rotateDisruptor = GetComponent<RotateDisruptor>();
    }


    void Update()
    {
        Vector3 forward = transform.TransformDirection(-Vector3.right) * 100;
        //  Debug.Log(transform.position+forward);
        Debug.DrawRay(transform.position, forward, Color.green);
    }



    public void InitiateRotations()
    {
        StartCoroutine(Rotations());
    }

    public IEnumerator Rotations()
    {

        Vector3 target_up = cam.position;
        target_up.y = transform.position.y;


        yield return StartCoroutine( rotateDisruptor.RotateTowards(target_up));

    


        Vector3 target_left = turretHead1.position;
        target_left.x = 0;
        target_left.y = transform.position.y;



        yield return StartCoroutine(rotateDisruptor.RotateTowards(target_left));


        Vector3 target_down = turretHead1.position;
        target_down.x = 0;



        yield return StartCoroutine(rotateDisruptor.RotateTowards(target_down));



        charge1.GetComponent<MoveDisruptorCharge>().StartMovement();
        charge2.GetComponent<MoveDisruptorCharge>().StartMovement();

        target_up = turretHead1.position;
        target_up.y = transform.position.y;

        yield return StartCoroutine(rotateDisruptor.RotateTowards(target_up));


        Vector3 target_right = cam.position;
        target_right.y = transform.position.y;

        yield return StartCoroutine(rotateDisruptor.RotateTowards(target_right));

        GetComponent<DisruptorStartEndMovement>().MoveDown();



    }


   




    



}
