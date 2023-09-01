using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDisruptorCharge : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] GameObject turret_control;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
       
    }

  public  void SetTargets(int i) 
    {
        switch (i) 
        {
            case 1:
                target = GameObject.FindGameObjectWithTag("TurretHead1").transform;
                turret_control = GameObject.FindGameObjectWithTag("TurretControl1");
                
                break;
            case 2:
                target = GameObject.FindGameObjectWithTag("TurretHead2").transform;
                turret_control = GameObject.FindGameObjectWithTag("TurretControl2");

                break;
        }
    }

    public void StartMovement()
    {
        StartCoroutine(Move());
    }


    IEnumerator Move() {

        while(Vector3.Distance (transform.position,  target.position) > 0.1)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            yield return null;
        }

        turret_control.GetComponent<ControlManager>().DisableFor(DifficultyManager.DISRUPTOR_DISABLE_TIME);
        Destroy(gameObject);
    }


   
   
}
