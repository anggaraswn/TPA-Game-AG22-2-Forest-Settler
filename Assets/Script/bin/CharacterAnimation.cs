using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator animator;
    private Vector3 moveDirection;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //if (direction.magnitude >= 0.1f)
        //{
        //    animator.SetBool("IsIdle", false);
        //    animator.SetBool("IsWalking", true);

            //    animator.SetFloat("DirectionX", direction.x);
            //    animator.SetFloat("DirectionY", Mathf.Abs(direction.z));
            //}
            //else
            //{
            //    animator.SetBool("IsIdle", true);
            //    animator.SetBool("IsWalking", false);
            //}

        if(direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            //walk
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsSprinting", false);

            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", Mathf.Abs(direction.z));

            if (Input.GetMouseButton(1))
            {
                animator.SetBool("IsRolling", true);
                animator.SetBool("IsWalking", false);
            }
            else
            {
                animator.SetBool("IsRolling", false);
            }
        }
        else if(direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            //run
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsSprinting", true);
            animator.SetBool("IsWalking", false);

            animator.SetFloat("DirectionX", 0f);
            animator.SetFloat("DirectionY", 2);

            if (Input.GetMouseButton(1))
            {
                animator.SetBool("IsRolling", true);
                animator.SetBool("IsSprinting", false);
            }
            else
            {
                animator.SetBool("IsRolling", false);
            }
        }
        else if(direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift) && direction.z == 0)
        {
            //strafe
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsSprinting", false);

            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", 0f);

        }
        else
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsSprinting", false);


            animator.SetFloat("DirectionX", 0f);
            animator.SetFloat("DirectionY", 0f);

        }
    }

    //private void move()
    //{
    //    float moveZ = Input.GetAxisRaw("Vertical");
    //    float moveX = Input.GetAxisRaw("Horizontal");


    //    moveDirection = new Vector3(moveX, 0, moveZ);

    //    if(moveDirection != Vector3.zero && moveZ != 0 && !Input.GetKey(KeyCode.LeftShift))
    //    {
    //        //walk
    //    }else if (moveDirection != Vector3.zero && moveZ != 0 && Input.GetKey(KeyCode.LeftShift))
    //    {
    //        //run

    //    }
    //    this.transform.Translate(Input.GetAxis("Horizontal"), 0, 0);
    //    else if(moveDirection != Vector3.zero && moveZ = 0)
    //    {

    //    }

       
    //}
}
