using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseZoneSystem : MonoBehaviour
{
    public EnemyController ctx;
    public GameObject target;

    private void FixedUpdate()
    {
        if(target != null)
        {
            if(Physics.Raycast(transform.position, target.transform.position, ctx.childLayer))
            {
                if (ctx.currState != ctx.ChasingState)
                {
                    ctx.currChildChasing = target;
                    ctx.SwitchState(ctx.ChasingState);
                    //ctx.isChasing = true;
                }
                
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Child"))
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Child") && target == null)
        {
            target = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == target)
        {
            //ctx.isChasing = false;
            target = null;
            if(ctx.currState != ctx.AttackingState) ctx.SwitchState(ctx.SearchingState);

        }
    }
}