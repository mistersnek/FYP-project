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

    
    float speed = 5f;
    [Header("Walking Speed")]
    public float currentSpeed;

    [Header("How smooth turning is")]
    public float turnSmoothing = 0.3f;
    float turnVelocity;

    [HideInInspector]
    public Vector3 moveDir;

    [Header("Animations")]
    public GameObject playerCharacter;
    private Animator anim;

    public bool isCrouched;

    [Header ("Gravity Stuff")]
    [SerializeField]
    private float gravity = 9f;

    void Start()
    {
        currentSpeed = speed;
        //Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponentInChildren<Animator>();
        Physics.IgnoreLayerCollision(6,8);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform child in transform)
        {
            if(child.name == "Player Character")
            {
                child.localPosition = Vector3.zero;
            }
        }


        //assign vertical and horizontal keys to a vector 3
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        Run();

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
            }
        }

        if (direction.magnitude >= 0.1f)
        {
            //points the character to where the camera is pointing
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //smoothing of the turning of the player
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnSmoothing);
            //apply smoothness angle
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //move the player according to the key pressed
            controller.Move(moveDir.normalized * currentSpeed * Time.deltaTime);

            if (currentSpeed > speed)
            {
                anim.SetFloat("Speed", 1.0f, 0.1f, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
                anim.SetFloat("Velocity", 1.0f, 0.1f, Time.deltaTime);
            }

        }
        else
        {
            anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
            anim.SetFloat("Velocity", 0f, 0.1f, Time.deltaTime);
        }

        direction.y -= gravity * Time.deltaTime;
        controller.SimpleMove(direction * Time.deltaTime);
    }

    public void Crouch()
    {
        anim.SetBool("crouching", true);
        controller.height = 1.43f;
        controller.radius = 0.54f;
        controller.center = new Vector3 (0f, 0.7f, -0.09f);
        isCrouched = true;
    }
    public void Stand()
    {
        anim.SetBool("crouching", false);
        controller.height = 1.72f;
        controller.radius = 0.34f;
        controller.center = new Vector3 (0f, 0.89f, 0.06f);
        isCrouched = false;
    }
    public void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && isCrouched == false)
            currentSpeed = speed * 1.5f;
        else
            currentSpeed = speed;
    }
}