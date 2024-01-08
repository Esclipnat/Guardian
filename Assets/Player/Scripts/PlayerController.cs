using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerInput input;
    public Animator animController;
    private InputAction move;
    private InputAction sprint;
    private InputAction mouse;
    private Vector3 moveDir;
    private Vector2 mousePos;
    public CharacterController charCon;
    public float walkSpeed;
    public float runSpeed;
    public float currentSpeed;
    public bool canSprint;
    public bool isDashing;
    public AudioSource SFXCandy;

    public Image staminaCircle;
    public float timeSprinted;
    public float maxSprintTime;

    public Image attackCircle;
    public AttackSystem attackSys;



    // int candiesInHand; //determines how many candies player has
    /* Define los límites del movimiento
    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;
    */

    private void Awake()
    {
        move = input.actions["Move"];
        sprint = input.actions["Sprint"];
        mouse = input.actions["MousePosition"];


        attackSys.onAttack += DashForward;
        //candiesInHand = 0;
        canSprint = true;
    }

    private void FixedUpdate()
    {
        Movement();
        MouseRotation();
        UpdateCircles(); //Update stamina and attack circles

        
    }

    private void Movement()
    {
        if (!isDashing)
        {
            timeSprinted = Mathf.Clamp(timeSprinted, 0, maxSprintTime);
            if (sprint.IsPressed() && canSprint)
            {
                timeSprinted += Time.deltaTime;
                currentSpeed = runSpeed;
            }
            else
            {
                timeSprinted -= Time.deltaTime;
                currentSpeed = walkSpeed;
            }

            if (timeSprinted >= maxSprintTime)
            {
                canSprint = false;
               //staminaCircle.color = Color.yellow;
            }
            else if (timeSprinted <= maxSprintTime / 2)
            {
                //staminaCircle.color = Color.green;
                canSprint = true;
            }


            moveDir = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);

            Vector3 newPosition = transform.position + Time.fixedDeltaTime * currentSpeed * 50f * moveDir;
            //newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            //newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

            if(moveDir != Vector3.zero)
            {
                animController.SetBool("Walk", true);
                animController.SetBool("Idle", false);
            }
            else
            {
                animController.SetBool("Walk", false);
                animController.SetBool("Idle", true);
            }


            charCon.SimpleMove(newPosition - transform.position);
        }
        else
        {
            Vector3 newPosition = transform.position + Time.fixedDeltaTime * currentSpeed * 200f * transform.forward;
            charCon.SimpleMove(newPosition - transform.position);
        }

        staminaCircle.fillAmount = 1 - timeSprinted/maxSprintTime;

    }

    private void MouseRotation()
    {
        
        mousePos = mouse.ReadValue<Vector2>();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        Plane xzPlane = new Plane(Vector3.up, Vector3.zero);
        if (xzPlane.Raycast(ray, out float distance))
        {
            // Obtain intersection point
            Vector3 intersectionP = ray.GetPoint(distance);
            Vector3 lookDir = intersectionP - transform.position;
            lookDir.y = 0; // make sure for player to not rotate in y axis

            // if mouse is looking at some plane
            if (lookDir != Vector3.zero & !isDashing)
            {
                transform.rotation = Quaternion.LookRotation(lookDir);
            }
        }

    }

    private void DashForward()
    {
        isDashing = true;
        animController.SetBool("Attack", true);

        StartCoroutine(ReactivateMovement(.2f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CandyContainer"))
        {
            SFXCandy.Play();
            //candiesInHand += 1; 
            GameManagerController.instance.candy += 1;
            Destroy(other.gameObject);
        }
    }

    IEnumerator ReactivateMovement(float time)
    {
        yield return new WaitForSeconds(time);
        isDashing = false;
        animController.SetBool("Attack", false);
    }


    public void DeathAnim()
    {
        animController.SetBool("Dead", true);
    }

    private void UpdateCircles()
    {
        if (canSprint) staminaCircle.color = Color.green;
        else staminaCircle.color = Color.yellow;

        if (attackSys.canAttack) attackCircle.color = Color.green;
        else attackCircle.color = Color.yellow;
    }
}


