
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public float currAngleEuler;
    public float angleVariation; //How much the angle varies in eulers (max 1)
    public float angleVariationInterval;
    public float rayToWallDistance; //Controls the distance of the raycast that checks if the wall is on sight
    public RaycastHit hit;
    public LayerMask childLayer;
    public LayerMask wallLayer;
    public ChaseZoneSystem chaseSys;

    public float rotationSpeed;

    public GameObject currChildChasing;
    public float currSpeed;
    public float speed;
    public CharacterController charCon;

    public GameObject candyPrefab;
    public GameObject candyContainerPrefab;

    public Action onChildTouched;
    public Action onChildKilled;
    public GameObject currChildAttacking;
    public float attackTime;
    public float timeAttacked;
    public float timeToKillChild;

    public Collider col;

    public EnemyBaseState currState;
    public EnemySpawningState SpawningState = new EnemySpawningState();
    public EnemySearchingState SearchingState = new EnemySearchingState();
    public EnemyChasingState ChasingState = new EnemyChasingState();
    public EnemyAvoidingWallState AvoidingWallState = new EnemyAvoidingWallState();
    public EnemyAttackingState AttackingState = new EnemyAttackingState();
    public EnemyDyingState DyingState = new EnemyDyingState();

    public AudioSource SFXTarget;
    public AudioSource SFXKillChild;

    public Animator animController;
    public ParticleController particleController;

    private void Start()
    {
        angleVariation = (angleVariation >= 1) ? angleVariation = 0.999f : angleVariation;
        currAngleEuler = 0;

        AdjustDifficulty(); //adjust difficulty in regards to current points
        currSpeed = speed;
        currState = SpawningState;
        currState.EnterState(this);
    }

    private void FixedUpdate()
    {
        currState.UpdateState();
        Debug.Log(currState);
        if(currState != SpawningState) charCon.SimpleMove(Time.fixedDeltaTime * currSpeed * 50f * transform.forward);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currState.ExitState();
        currState = state;
        state.EnterState(this);
    }

    public void RotateTowards(Vector3 targetPos)
    {
        Vector3 targetDir = targetPos - transform.position;
        float angle = Vector3.Angle(transform.forward, targetDir);

        // Si el ángulo no es muy pequeño (puedes ajustar el umbral), sigue rotando
        if (angle > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChildAttackZone"))
        {
            if((currState != AttackingState || currState != DyingState) && currChildAttacking == null)
            {
                currChildAttacking = other.GetComponent<EnemyCheckerSystem>().ctx.gameObject;
                SwitchState(AttackingState);
                SFXTarget.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*
        if (other.gameObject == currChildAttacking)
        {
            if (currState == AttackingState)
            {
                currChildAttacking = null;
                SwitchState(SearchingState);
            }
        }
        */
    }

    private void AdjustDifficulty()
    {

        float originalSpeed = speed;
        speed += Random.Range(0, GameManagerController.instance.currPoints * .01f); // If currpoints == 100, it can get up to 1 speed, for example.
        speed = Mathf.Clamp(speed, originalSpeed, 14f); //last value determines max speed
        float originalTimeToKillChild = timeToKillChild;
        timeToKillChild -= Random.Range(0, GameManagerController.instance.currPoints * .005f); // If currpoints == 100, it can get down up to .5, for example.
        timeToKillChild = Mathf.Clamp(timeToKillChild, 1.5f, originalTimeToKillChild); //min 1.5

    }

    public void Die()
    {
        SwitchState(DyingState);
    }
    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    public void KillChild()
    {
        SFXKillChild.Play();
        Destroy(currChildAttacking);
    }


}
