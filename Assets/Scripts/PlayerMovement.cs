using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{

    public Transform sensor;

    private int environmentLayerMask; // Layer for raycast to hit
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _renderer;

    private Transform _transform;

    //Jump Stuff 
    public AnimationCurve gravityRise;

    public float initialJumpForce;
    public float gravityOnRelease;

    private float dashStarted;
    public float dashCooldown;
    public float dashDuration;
    public float dashPower;
    private bool dash;

    public float groundCheckDistance = 0.2f;

    public AudioSource audioSource;

    public AudioClip[] dashSounds;

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

    public float windMultiplier = 10;

    private bool isInWind = false;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        this._renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        this._transform = GetComponent<Transform>();
        this.environmentLayerMask = LayerMask.GetMask("Environment");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        RawHorizontal = Input.GetAxisRaw("Horizontal");

        this.movement = new Vector3(horizontalInput * Speed * 100, 0.0f, 0.0f);

        if (Input.GetButtonDown("Jump"))
        {
            this.jumpPressed = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            this.jumpPressed = false;
        }

        var isDashAvaillable = this.isDashAvaillable();

        if (Input.GetButtonDown("Dash"))
        {

            if (isDashAvaillable)
            {
                this.audioSource.PlayOneShot(this.dashSounds[this.getDashSoundIndex()]);
                this.dashStarted = Time.time;
                Dashing = true;
            }
        }

        if (this.rawHorizontal != 0)
        {
            this._renderer.flipX = this.rawHorizontal < 0;
        }
    }


    float getSpriteOrientation()
    {
        return this._renderer.flipX ? -1 : 1;
    }

    bool isDashAvaillable()
    {
        return this.dashStarted == 0 || (this.dashStarted - Time.time) <= -dashCooldown;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        var vel = this._rb.velocity;

        if (this.jumpPressed)
        {
            if (!this.jumping)
            {
                this.jumpStarted = Time.time;
                this.Jumping = true;
            }
        }



        var collidesWithGroundDebug = collidesWithGround();
        Debug.Log("CollidesWithGrounDebug: " + collidesWithGroundDebug);

        if (this.jumping && (this.jumpStarted - Time.time) > -0.2)
        {
            vel.y = this.gravityRise.Evaluate(Time.time - jumpStarted) * initialJumpForce;
        }
        else if (this.jumping && (this.jumpStarted - Time.time) > -0.3)
        {
            vel.y = 0;
        }
        else if (this.jumping && collidesWithGroundDebug)
        {
            Debug.Log("STOPPED JUMP");
            vel.y = 0;
            Jumping = false;
        }
        else if (this.jumping)
        {
            vel.y = -this.gravityOnRelease;
        }

        if (this.dash && (this.dashStarted - Time.time) >= -this.dashDuration)
        {
            vel.x = this.getSpriteOrientation() * dashPower * Time.fixedDeltaTime;
        }
        else
        {
            Dashing = false;

            if(this.isInWind) {
                vel.x = (this.movement.x * Time.fixedDeltaTime) + windMultiplier;
            } else {
                vel.x = this.movement.x * Time.fixedDeltaTime;
            }
        }

        this._rb.velocity = vel;
    }


    private bool collidesWithGround()
    {
        var position = this.sensor.position;
        var forward = Vector2.down;
        Debug.Log("forward: " + forward.x + " - " + forward.y);
        Debug.DrawRay(position, forward, Color.red, groundCheckDistance);
        var hit = Physics2D.Raycast(position, forward, groundCheckDistance, this.environmentLayerMask);
        return hit.collider != null;
    }




    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Wind"))
        {
            this.isInWind = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Wind"))
        {
            this.isInWind = false;
        }
    }

        private int getDashSoundIndex() {
        System.Random r = new System.Random();
        int rInt = r.Next(0, this.dashSounds.Length);
        return rInt;
    }
}
