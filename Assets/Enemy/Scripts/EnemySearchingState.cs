using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemySearchingState : EnemyBaseState
{

    public float time;
    public override void EnterState(EnemyController eC)
    {
        base.EnterState(eC);
        time = 0;
        ctx.currAngleEuler = Random.Range(-ctx.angleVariation, ctx.angleVariation);
        eC.animController.SetBool("walk", true);
        //ctx.StartCoroutine(NewTargetAngle());

    }
    public override void ExitState()
    {
        ctx.animController.SetBool("walk", false);
    }

    public override void UpdateState()
    {
        ctx.transform.Rotate(0, ctx.currAngleEuler, 0);
        time += Time.deltaTime;
        if (time >= ctx.angleVariationInterval)
        {
            ctx.currAngleEuler = Random.Range(-ctx.angleVariation, ctx.angleVariation);
            time = 0;
        }

        if (Physics.Raycast(ctx.transform.position, ctx.transform.forward, ctx.rayToWallDistance, ctx.wallLayer))
        {
            ctx.SwitchState(ctx.AvoidingWallState);
        }
        Debug.DrawLine(ctx.transform.position, ctx.transform.position + (ctx.transform.forward * ctx.rayToWallDistance), Color.red);

    }

    IEnumerator NewTargetAngle()
    {
        yield return new WaitForSeconds(ctx.angleVariationInterval);
        ctx.currAngleEuler = Random.Range(-ctx.angleVariation, ctx.angleVariation);
        ctx.StartCoroutine(NewTargetAngle());
    }
}
