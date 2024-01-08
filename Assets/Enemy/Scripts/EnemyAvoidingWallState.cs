using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAvoidingWallState : EnemyBaseState
{
    public float time;
    private float randTime;
    public override void EnterState(EnemyController eC)
    {
        base.EnterState(eC);
        time = 0;
        randTime = Random.Range(3f, 5.5f);
        
        if (Random.value < 0.5f) // Turn right or left when facing a wall
            ctx.currAngleEuler = 1;
        else
            ctx.currAngleEuler = -1;
        
    }
    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        ctx.transform.Rotate(0, ctx.currAngleEuler, 0);
        time += Time.deltaTime;

        
        if (time >= randTime)
        {
            ctx.SwitchState(ctx.SearchingState);
        }
        /*
        Debug.DrawLine(ctx.transform.position, ctx.transform.position + (ctx.transform.forward * ctx.rayToWallDistance), Color.green);
        */
    }
}