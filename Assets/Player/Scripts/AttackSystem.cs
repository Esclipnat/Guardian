using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class AttackSystem : MonoBehaviour
{
    public PlayerInput input;
    public InputAction attack;
    public Collider attackCollider;
    public Action onAttackPressed;
    public Action onAttack;
    public Action onAttackReleased;
    public bool canAttack;
    public float attackCD;
    public float attackDuration;
    public AudioSource SFXKill;
    public AudioSource SFXDash;

    public EnemyController justAttacked; //Saving the reference of an attacked enemy in order to avoid attacking the same enemy 2 frames
    

    private void Awake()
    {
        canAttack = true;
        onAttackPressed += () => {
            if (canAttack)
            {
                SFXDash.Play();
                onAttack?.Invoke();
                attackCollider.enabled = true;
                StartCoroutine(AttackOnCD());
            }
        };
        input.actions["Attack"].performed += ctx => onAttackPressed?.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy") & justAttacked != other?.GetComponent<EnemyController>()) //avoid destroying same object twice
        {
            Debug.Log("Enemigo out");
            SFXKill.Play();
            onAttackReleased?.Invoke();
            GameManagerController.instance.sumPoints(1);

            EnemyController enemyC = other.GetComponent<EnemyController>();
            justAttacked = enemyC;
            if(Random.value > 0.3f) // 70 percent chance to drop candy
            {
                //make sure the candy is not destroyed or moved position by making a container
                GameObject newCandyContainer = Instantiate(enemyC.candyContainerPrefab, enemyC.transform.position, Quaternion.identity);
                GameObject newCandy = Instantiate(enemyC.candyPrefab, newCandyContainer.transform.position, Quaternion.identity, newCandyContainer.transform);
                StartCoroutine(CandyActivateCollider(newCandyContainer.GetComponent<Collider>()));
            }
           

            if (enemyC.currChildAttacking != null)
            {
                ChildController currChAttC = enemyC.currChildAttacking.GetComponent<ChildController>(); //curr child attacking controller //i hate this
                currChAttC.isBeingAttacked = false;
            }

            other.GetComponent<EnemyController>().Die();
            
        }
    }

    IEnumerator AttackOnCD()
    {
        canAttack = false; // deactivate can attack
        yield return new WaitForSeconds(attackDuration); 
        attackCollider.enabled = false; //disable collider after attack duration passed
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }

    IEnumerator CandyActivateCollider(Collider col)
    {
        yield return new WaitForSeconds(.35f);
        col.enabled = true;
    } 
}
