using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawnerScript : MonoBehaviour
{
    public GameObject obj;
    public Transform Spawn1, Spawn2;
    public bool spawningBool = true;
    public float spawnTime;
    public float spawnRatio;

    void Start()
    {
        GameManagerController.instance.onPointsChanged += AdjustDifficulty;
        StartCoroutine(spawning());
    }
    IEnumerator spawning()
    {
        while (spawningBool)
        {
            yield return new WaitForSeconds(spawnTime);
            if (Random.value<0.5) 
            {
                Instantiate(obj, Spawn1.position, Spawn1.rotation);
            }
            else
            {
                Instantiate(obj, Spawn2.position, Spawn2.rotation);
            }
           
        }
        
    }

    private void AdjustDifficulty()
    {
        if (spawnTime >= 2)
        {
            spawnTime = spawnTime - (spawnRatio * GameManagerController.instance.currPoints);
        }
        
    }


}
