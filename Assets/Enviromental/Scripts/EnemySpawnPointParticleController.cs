using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointParticleController : MonoBehaviour
{
    private EnemyController currEC;
    public ParticleController particleC;


    public void ReceiveEnemy(EnemyController eC)
    {
        currEC = eC;
        particleC.Play();
        
    }

    private void FixedUpdate()
    {
        if (currEC != null)
        {
            if (currEC.currState == currEC.SpawningState)
            {
                particleC.transform.position = new Vector3(particleC.transform.position.x, currEC.transform.position.y, particleC.transform.position.z);
            }
            else
            {
                particleC.Stop();
                currEC = null;
            }

        }
    }

}
