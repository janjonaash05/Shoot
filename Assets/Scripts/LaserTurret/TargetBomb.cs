using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetBomb : MonoBehaviour
{



    public GameObject turret_charge_head;
    public GameObject turret_head;

    private Vector3 originVector;

    public bool isTargeting = false;
    public bool isBarraging;

    private GameObject laser;

    public void setupLaser()
    {



        laser = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(laser.GetComponent<Collider>());
        laser.GetComponent<Renderer>().sharedMaterial = turret_charge_head.GetComponent<Renderer>().material;
        laser.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        originVector = turret_charge_head.transform.position;


        isTargeting = true;













    }

    public void StartTargeting(GameObject Bomb)
    {
        try
        {
            GetComponent<RotateTurretHead>().EngageShootMode();
            setupLaser();

            StartCoroutine(Target(Bomb));
            
            Bomb.transform.GetComponent<DamageBomb>().StartDamage();
        }
        catch (Exception e)
        {
            Destroy(laser);


        }


    }


    public IEnumerator Target(GameObject bomb)
    {

        float i = 0;





        while (bomb != null)
        {

            i++;

            Vector3 targetPos;
            try
            {
                targetPos = bomb.transform.position;
            }
            catch (Exception e)
            {
                continue;
            }




            Track(targetPos, i * 0.001f);
            yield return null;
        }




        Destroy(laser);
        isTargeting = false;

        GetComponent<RotateTurretHead>().DisengageShootMode();



    }





    void Track(Vector3 targetVector, float sizeIncrease)
    {

        if (laser == null)
        {
            isTargeting = false;

            GetComponent<RotateTurretHead>().DisengageShootMode();
            return;
        }
        float distance = Vector3.Distance(originVector, targetVector);

        laser.transform.localScale = new Vector3(laser.transform.localScale.x + sizeIncrease, distance / 2f, laser.transform.localScale.z + sizeIncrease);

        Vector3 middleVector = (originVector + targetVector) / 2f;
        laser.transform.position = middleVector;

        Vector3 rotationDirection = (targetVector - originVector);
        laser.transform.up = rotationDirection;

    }



    GameObject[] GetAllPossibleTargets(string tag)
    {
        GameObject[] allTargets = GameObject.FindGameObjectsWithTag(tag);
        Material mat = transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;


        var coloredTargets = (
            from bomb in allTargets
            where bomb.GetComponent<Renderer>().sharedMaterials[1].color == mat.color
            select bomb
        );

        return coloredTargets.ToArray();
    }


    GameObject turret_station_charge;

    public void EngageBarrageStart(string tag, GameObject turret_station_charge)
    {


        Debug.Log("isBarraging " + isBarraging);
        this.turret_station_charge = turret_station_charge;
        if (!isBarraging)
        {
            StartCoroutine(BarrageStart(tag));

        }
    }

    IEnumerator BarrageStart(string tag)
    {
        isBarraging = true;
        yield return StartCoroutine(Barrage(GetAllPossibleTargets(tag)));
        isBarraging = false;
    }

    IEnumerator Barrage(GameObject[] targets)
    {
        int i = targets.Count() - 1;
        if (targets.Count() == 0) { yield break; }

        turret_station_charge.GetComponent<DecreasePower>().Decrease();


        while (i > -1)
        {
            float speed = 0;
            try
            {
                speed = targets[i].GetComponent<DamageBomb>().damageSpeed;
                StartTargeting(targets[i]);


                ScoreCounter.Increase(1);


                i--;

            }
            catch (Exception e)
            {
                isTargeting = false;

                GetComponent<RotateTurretHead>().DisengageShootMode();
                break;
            }
            yield return new WaitForSeconds(speed * 10);

        }
    }

}
