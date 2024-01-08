using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState
{
    protected EnemyController ctx;
    public virtual void EnterState(EnemyController eC)
    {
        ctx = eC;
    }
    public abstract void ExitState();
    public abstract void UpdateState();
}
