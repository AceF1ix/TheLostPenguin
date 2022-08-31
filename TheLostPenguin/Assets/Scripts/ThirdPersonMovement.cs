using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    private CharacterController controller;
    public Transform cam;
    private float speed = 20f;
    private float gravity = 20f;
    private float jumpForce = 18f;
    private float ADDITIONAL_FALL_GRAVITY = 0.36f;
    public float turnSmoothTime = 0.1f;
    private Animator anim;
    Vector3 velocity;
    float turnSmoothVelocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    private Vector3 initialPosition;
    public WaterRising waterRising;

    // Update is called once per frame
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        waterRising = GameObject.Find("Plane").GetComponent<WaterRising>();
        initialPosition = transform.position;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("InAir", true);
            velocity.y = jumpForce;
        }
        else
        {
            anim.SetBool("InAir", false);

            if (velocity.y < 6 && isGrounded!=true){
                velocity.y -= ADDITIONAL_FALL_GRAVITY;
                }
        }
        
        velocity.y -= gravity * Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, -40f, 100f);
        controller.Move(velocity * Time.deltaTime);

        if (direction.magnitude >= 0.5 && isGrounded)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "water")
        {
            // Reset player position on contact with water

            controller.enabled = false;
            transform.position = initialPosition;
            controller.enabled = true;
            transform.rotation = Quaternion.identity;
            velocity = Vector3.down * 0.1f;

            // Reset water level

            waterRising.restart_pos();

        }
    }
}
