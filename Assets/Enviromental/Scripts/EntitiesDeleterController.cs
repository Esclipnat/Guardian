using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesDeleterController : MonoBehaviour
{
    //THIS IS JUST FOR DELETING ENTITIES IN CASE THEY FALL OFF THE MAP
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
