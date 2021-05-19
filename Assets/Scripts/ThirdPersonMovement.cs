using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public float initialMoveSpeed = 6f;
    public float movespeed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    public float gravity = -9.81f;
    public float initialJumpHeight = 3f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    public int jmpcount=2;
    Vector3 velocity;
    bool isGrounded;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public ParticleSystem Speed_effect;
    public ParticleSystem Jump_effect;


    private void Start()
    {
        movespeed = initialMoveSpeed;
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jmpcount = 2;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)

        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * movespeed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump")&& jmpcount!=0) //&& isGrounded )
        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jmpcount = jmpcount-1;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (velocity.y < 0)
        {
            velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (velocity.y > 0 && !Input.GetButton("Jump"))
        {
            velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag)
        {
            case "SpeedBoost":
                movespeed = 25f;
                Speed_effect.Play();
                break;
            case "JumpBoost":
                jumpHeight = 9f;
                Jump_effect.Play();
                break;
            case "Ground":
                movespeed = Mathf.SmoothStep(movespeed, initialMoveSpeed, Time.deltaTime*20);
                jumpHeight = initialJumpHeight;
                Speed_effect.Stop();
                Jump_effect.Stop();
                break;
        }
    }

   
}
