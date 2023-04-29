using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D _rb;

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
        this.movement = new Vector3(horizontalInput * Speed * 100, 0.0f, 0.0f);

    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {   

        var vel = this._rb.velocity;
        vel.x = this.movement.x * Time.fixedDeltaTime;
        this._rb.velocity = vel;
    }
}
