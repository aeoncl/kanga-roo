using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _renderer;

    //Jump Stuff 
    public AnimationCurve gravityRise;

    public float initialJumpForce;
    public float gravityOnRelease;
    
    private float dashStarted;
    public float dashCooldown;
    public float dashDuration;
    public float dashPower;
    private bool dash;

    public bool Dashing
    {
        set
        {
            dash = value;
            _animator.SetBool("IsDashing", value);
        }
    }

    private float jumpStarted;
    private bool jumpPressed;
    
    private bool jumping;

    public bool Jumping
    {
        set
        {
            jumping = value;
            _animator.SetBool("IsJumping", value);
        }
    }

    private float rawHorizontal;
    public float RawHorizontal
    {
        set
        {
            rawHorizontal = value;
            _animator.SetFloat("Speed", Mathf.Abs(value));
        }
    }

    //Movement
    public float Speed = 50;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        this._renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        RawHorizontal = Input.GetAxisRaw("Horizontal");
        this.movement = new Vector3(horizontalInput * Speed * 100, 0.0f, 0.0f);

        if (Input.GetButtonDown("Jump")){
            this.jumpPressed = true;
        }

        if (Input.GetButtonUp("Jump")){
            this.jumpPressed = false;
        }

        var isDashAvaillable = this.isDashAvaillable();

        if(Input.GetButtonDown("Dash")) {

            if(isDashAvaillable) {
                this.dashStarted = Time.time;
                Dashing = true;
            }
        }  

        if(this.rawHorizontal != 0 ){
            this._renderer.flipX = this.rawHorizontal < 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("IsAttacking");
            _animator.SetBool("IsJumping", false);
        }
    }


    float getSpriteOrientation() {
        return this._renderer.flipX ? -1 : 1;
    }

    bool isDashAvaillable() {
        return this.dashStarted == 0 || (this.dashStarted - Time.time) <= -dashCooldown;
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
               this.Jumping = true;
            }
        }   

        if(this.jumping && (this.jumpStarted - Time.time) > -0.2) {
            vel.y = this.gravityRise.Evaluate(Time.time - jumpStarted) * initialJumpForce;
        } else if (this.jumping && (this.jumpStarted - Time.time) > -0.25)
        {
            vel.y = 0;
        } else if (this.jumping) {
            vel.y = -this.gravityOnRelease;
        }

        if(this.dash && (this.dashStarted - Time.time) >= -this.dashDuration) {
            vel.x = this.getSpriteOrientation() * dashPower * Time.fixedDeltaTime;
        } else {
            Dashing = false;
            vel.x = this.movement.x * Time.fixedDeltaTime;
        }

        this._rb.velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            Jumping = false;
        }
    }
}
