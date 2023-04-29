using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    //Jump Stuff 
    public AnimationCurve gravityRise;

    public float initalJumpForce;
    public float gravityOnRelease;
    
    private float dashStarted;
    public float dashCooldown;
    public float dashDuration;
    public float dashPower;
    private bool dash;


    private float jumpStarted;
    private bool jumpPressed;
    private bool jumping;


    private float rawHorizontal;

    //Movement
    public float Speed = 50;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rawHorizontal = Input.GetAxisRaw("Horizontal");
        this.movement = new Vector3(horizontalInput * Speed * 100, 0.0f, 0.0f);

        if (Input.GetButtonDown("Jump")){
            this.jumpPressed = true;
        }

        if (Input.GetButtonUp("Jump")){
            this.jumpPressed = false;
        }

        if(Input.GetButtonDown("Dash")) {

            if(this.dashStarted == 0 || (this.dashStarted - Time.time) <= -dashCooldown) {
                this.dashStarted = Time.time;
                this.dash = true;
            }
        }

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {   
        var vel = this._rb.velocity;

        if(this.jumpPressed) {
            if(!this.jumping && vel.y >= -0.9) {
               this.jumpStarted = Time.time;       
               this.jumping = true;
            }
        }

        if(this.jumping && (this.jumpStarted - Time.time) > -0.2) {
            vel.y = this.gravityRise.Evaluate(Time.time - jumpStarted) * initalJumpForce;
        } else if (this.jumping && (this.jumpStarted - Time.time) > -0.25) {
            vel.y = 0;
        }else if (this.jumping) {
            vel.y = -this.gravityOnRelease;
            this.jumping = false;
        }

        if(this.dash && (this.dashStarted - Time.time) >= -this.dashDuration) {
            vel.x = this.rawHorizontal * dashPower * Time.fixedDeltaTime;
        } else {
            this.dash = false;
            vel.x = this.movement.x * Time.fixedDeltaTime;
        }

        this._rb.velocity = vel;
    }
}
