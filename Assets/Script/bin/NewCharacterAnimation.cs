using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterAnimation : MonoBehaviour
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

        if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            //walk
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsSprinting", false);
            animator.SetBool("IsIdle", false);

            animator.SetFloat("DirectionX", direction.x);
            animator.SetFloat("DirectionY", Mathf.Abs(direction.z));

            if (direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                //run
                animator.SetBool("IsSprinting", true);
                animator.SetBool("IsWalking", false);
                animator.SetBool("IsIdle", false);
            }
        }
        else
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsSprinting", false);
            animator.SetBool("IsWalking", false);
        }
    }
}
