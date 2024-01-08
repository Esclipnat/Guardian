using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    public float time;
    private ChildController chC;
    public override void EnterState(EnemyController eC)
    {
        base.EnterState(eC);
        time = 0;
        ctx.currSpeed = 0;
        ctx.chaseSys.enabled = false;
        chC = ctx.currChildAttacking.GetComponent<ChildController>();
        chC.isBeingAttacked = true;
        ctx.animController.SetBool("Attack", true);

    }
    public override void ExitState()
    {
        ctx.currSpeed = ctx.speed;
        ctx.chaseSys.enabled = true;
        if(chC != null) chC.isBeingAttacked = false; //exception
        ctx.animController.SetBool("Attack", false);
    }

    public override void UpdateState()
    {
        time += Time.deltaTime;
        if(time >= ctx.timeToKillChild)
        {
            Debug.Log("killed child");
            ctx.KillChild();
            GameManagerController.instance.sumPoints(-2);
            GameManagerController.instance.LoseHP();
            ctx.SwitchState(ctx.SearchingState);

        }
        if(ctx.currChildAttacking == null){
            ctx.SwitchState(ctx.SearchingState);
            }
    }
}
