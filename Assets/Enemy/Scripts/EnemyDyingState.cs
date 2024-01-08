using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDyingState : EnemyBaseState
{
    public override void EnterState(EnemyController eC)
    {
        base.EnterState(eC);
        ctx.animController.SetTrigger("Dying");
        ctx.currSpeed = 0;
        ctx.chaseSys.enabled = false;
        ctx.StartCoroutine(WaitForDestroy());
        BloodSpawnerController.instance.SpawnNewBlood(ctx.transform.position);
    }
    public override void UpdateState() { }

    public IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(1);
        ctx.DestroyThis();
    }
    public override void ExitState() { }
}
