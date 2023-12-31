using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisruptor : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject prefab;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame


    IEnumerator Spawn()
    {

        while (true)
        {
            //if (GameObject.FindGameObjectsWithTag("Disruptor".Length)) { }

            int chance = 100 / DifficultyManager.DISRUPTOR_SPAWN_CHANCE;


            if (new System.Random().Next(0, chance) != 0) { yield return new WaitForSeconds(DifficultyManager.DISRUPTOR_SPAWN_DELAY);  continue; }

         GameObject b =    Instantiate(prefab, transform.position, prefab.transform.rotation);



            b.transform.GetChild(0).GetComponent<MoveDisruptorCharge>().SetTargets(1);

            b.transform.GetChild(1).GetComponent<MoveDisruptorCharge>().SetTargets(2);

            yield return new WaitForSeconds(DifficultyManager.DISRUPTOR_SPAWN_DELAY);
               
        }
    
    
    }
}
