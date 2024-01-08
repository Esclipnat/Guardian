using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public override void EnterState(EnemyController eC)
    {
        base.EnterState(eC);
        eC.animController.SetBool("Run", true);
    }
    public override void ExitState()
    {
        ctx.animController.SetBool("Run", false);
    }

    public override void UpdateState()
    {
        if (ctx.currChildChasing != null)
        {
            ctx.RotateTowards(ctx.currChildChasing.transform.position);
        }else ctx.SwitchState(ctx.SearchingState);

    }
}
