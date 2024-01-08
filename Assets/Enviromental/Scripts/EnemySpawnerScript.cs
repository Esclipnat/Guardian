using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject obj;
    public List<GameObject> spawners;
    public bool spawningBool = true;
    public float spawnTime;
    public float spawnRatio;
    //public Action<EnemyController> onEnemySpawned;

    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("EnemySpawner").ToList<GameObject>();
        /*
        spawners.ForEach(x => {
            onEnemySpawned += x.GetComponent<EnemySpawnPointParticleController>().ReceiveEnemy;
            });
        */
        GameManagerController.instance.onPointsChanged += AdjustDifficulty;
        StartCoroutine(spawning());
    }
    IEnumerator spawning()
    {
        while (spawningBool)
        {
            yield return new WaitForSeconds(spawnTime);
            
            int index = Random.Range(0, spawners.Count);
            EnemyController eC = Instantiate(obj, spawners[index].transform.position, spawners[index].transform.rotation).GetComponent<EnemyController>();
            spawners[index].GetComponent<EnemySpawnPointParticleController>().ReceiveEnemy(eC);

        }

    }

    private void AdjustDifficulty()
    {
        if (spawnTime >= 1.5)
        {
            spawnTime = spawnTime - (spawnRatio * GameManagerController.instance.currPoints);
        }

    }


}

