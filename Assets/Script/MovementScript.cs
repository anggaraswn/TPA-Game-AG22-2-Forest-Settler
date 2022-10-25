using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    private CharacterController controller;

    private float defaultSpeed = 6f;

    [SerializeField]
    private float speed = 6f;

    [SerializeField]
    private float maxRotateSpeed = 0.1f;

    [SerializeField]
    private Transform camera;

    private float currentVelocity;
    private float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 moveDirection = new Vector3();

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, maxRotateSpeed);

            moveDirection = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            moveDirection = moveDirection.normalized;

            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        moveDirection.y += (gravity * -1);
        moveDirection.x *= speed;
        moveDirection.z *= speed;
        controller.Move(moveDirection * Time.deltaTime);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 15f;
        }else if (!Input.GetKey(KeyCode.LeftShift)){
            speed = 6f;
        }
    }

    
}
