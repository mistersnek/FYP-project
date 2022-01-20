//Code adapted from:
//Vubi GameDev - https://www.youtube.com/watch?v=vTNWUbGkZ58

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovement : MonoBehaviour
{

    public GameObject particle;
    public GameObject player;

    ThirdPersonMovement moveScript;

    [Header("")]
    public float dashSpeed;
    public float dashTime;
   
    private float dashCooldown;
    [Header("Dash cooldown in seconds")]
    public float cooldown = 1;

    bool canDash = true;


    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<ThirdPersonMovement>();
        dashCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        bool dash = Input.GetKeyDown(KeyCode.LeftControl);

        if(canDash == true)
        {
            if (dash)
            {
                StartCoroutine(Dash());
                Instantiate(particle, player.transform.position, Quaternion.identity);
                StartCoroutine(DashTimer(dashCooldown));
            }
        }           
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);
            canDash = false;
            yield return null;
        }
    }

    IEnumerator DashTimer(float dashCooldown)
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}