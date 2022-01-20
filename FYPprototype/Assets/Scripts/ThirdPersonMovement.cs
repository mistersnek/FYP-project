//Code adapted from:
//Brackeys - https://www.youtube.com/watch?v=4HpC--2iowE

using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [Header("Drag player controller here")]
    public CharacterController controller;

    [Header("Drag scene camera here (NOT CINEMACHINE CAM)")]
    //cam references used for pointing the cam where the player is facing
    public Transform cam;

    [Header("Walking Speed")]
    float speed = 4f;
    public float currentSpeed;

    [Header("How smooth turning is")]
    public float turnSmoothing = 0.3f;
    float turnVelocity;

    [HideInInspector]
    public Vector3 moveDir;

    [Header("Animations")]
    public GameObject playerCharacter;
    private Animator anim;

    bool isCrouched;

    public float gravity = 20.0f;

    void Start()
    {
        currentSpeed = speed;
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //assign vertical and horizontal keys to a vector 3
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        Run();

        if (direction.magnitude >= 0.1f)
        {
            //points the character according to the key pressed
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //smoothing of the turning of the player
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //move the player according to the key pressed
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);

            if(currentSpeed > speed)
            {
                anim.SetFloat("Speed", 1.0f, 0.1f, Time.deltaTime);
            } else
            {
                anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
            }
            

        }
        else anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);

        /*if (Input.GetButton("Jump"))
        {
            Debug.Log("space was pressed");
            direction.y = 5f;
            direction.y -= gravity * Time.deltaTime;
            controller.Move(direction * Time.deltaTime);
        }*/

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouched)
            {
                Stand();
                isCrouched = false;
            }
            else
            {
                Crouch();
                isCrouched = true;
                if (Input.anyKeyDown)
                    anim.SetBool("crouchWalking", true);
                else if (!Input.anyKeyDown)
                    anim.SetBool("crouchWalking", false);
            }
        }
    }

    public void Crouch()
    {
        anim.SetBool("crouching", true);
        controller.height = 0.5f;        
    }
    
    public void Stand()
    {
        anim.SetBool("crouching", false);
        controller.height = 2.74f;
    }

    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isCrouched == false)
            currentSpeed = speed * 1.2f;
        else
            currentSpeed = speed;
    }
}