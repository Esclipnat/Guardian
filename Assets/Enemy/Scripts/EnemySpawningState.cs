using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningState : EnemyBaseState
{
    public RaycastHit hit;
    private Vector3 moveDir;
    public override void EnterState(EnemyController eC)
    {
        base.EnterState(eC);
        eC.animController.SetBool("Emerge", true);
        ctx.chaseSys.enabled = false;
        ctx.col.enabled = false;

    }
    public override void ExitState()
    {
        ctx.chaseSys.enabled = true;
        ctx.col.enabled = true;
        ctx.animController.SetBool("Emerge", false);

    }

    public override void UpdateState()
    {
        moveDir = Vector3.up + Random.Range(-1, 1) * Vector3.right + Random.Range(-1, 1) * Vector3.forward;
        ctx.charCon.Move(Time.fixedDeltaTime * moveDir * 2f);

        if(Physics.Raycast(ctx.transform.position, Vector3.down, out hit))
        {
            if (hit.distance > 1.6f) ctx.SwitchState(ctx.SearchingState);
        }
        

    }

}
