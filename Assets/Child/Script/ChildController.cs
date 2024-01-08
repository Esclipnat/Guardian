using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class ChildController : MonoBehaviour
{
    private GameObject house;
    public GameObject salida1;
    public GameObject salida2;  
    public NavMeshAgent navMesh;
    public bool isBeingAttacked;
    public bool hasCandy;
    public float currVel;
    public float vel;
    private void Start()
    {
        house = GameObject.FindGameObjectWithTag("House");
        salida1 = GameObject.FindGameObjectWithTag("Salida1");
        salida2 = GameObject.FindGameObjectWithTag("Salida2");

        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = currVel;
        navMesh.SetDestination(house.transform.position);
    }
    private void FixedUpdate()
    {
        navMesh.speed = currVel;

        if (isBeingAttacked)
        {
            currVel = 0;
            navMesh.velocity = Vector3.zero;
        }
        else
        {
            currVel = vel;
        }
        
        if (hasCandy)
        {
            
            if (Random.value<0.5){
                navMesh.SetDestination(salida1.transform.position);
            }
            else
            {
                navMesh.SetDestination(salida2.transform.position);
            }
            hasCandy = false;
            vel += 10;
            transform.gameObject.tag = "ChildHasCandy";
            transform.gameObject.layer = 8;

        }

       

    }

}

    