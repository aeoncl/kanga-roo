using System;
using UnityEngine;

public class EggController : MonoBehaviour
{
    [SerializeField] private float jumpPower = 10;

    private Rigidbody2D rigidBody;
    private Vector2 direction;
    private bool collidesWithPlayer;

    private bool doUpdate = false;

    private float btnPressTimer;

    public float btnPressTimeout = 0.5f;
    public float btnDoubleTapTimeout = 0.5f;
    public float torque = 10f;

    public Animator playerAnimator;



    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            Debug.Log("Delta: " + (this.btnPressTimer - Time.time) + "<= timeout: -" + "compar: " + ((this.btnPressTimer - Time.time) <= this.btnPressTimeout));

            if (this.btnPressTimer == 0 || (this.btnPressTimer - Time.time) <= -btnDoubleTapTimeout)
            {
                playerAnimator.SetTrigger("IsAttacking");
                playerAnimator.SetBool("IsJumping", false);

                this.doUpdate = true;
                var horizontalAxis = Input.GetAxis("Horizontal");
                direction = new Vector2(horizontalAxis, 1).normalized;
                this.btnPressTimer = Time.time;
                Debug.Log("DO UPDATE");
            }


        }


        if (Input.GetKeyUp(KeyCode.E) || (this.btnPressTimer - Time.time) <= -btnPressTimeout)
        {
            Debug.Log("STOP UPDATE");
            this.doUpdate = false;
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {

        Debug.Log("DoUpdate: " + doUpdate + " CollidesWithPlayer: " + collidesWithPlayer + " check: " + (this.btnPressTimer - Time.time) + " timeout: " + this.btnDoubleTapTimeout);

        if (doUpdate && collidesWithPlayer)
        {

            Debug.Log("FixedUpdate!");
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
            rigidBody.AddForce(direction * jumpPower, ForceMode2D.Impulse);
            rigidBody.AddTorque(this.torque);
            collidesWithPlayer = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collidesWithPlayer = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
            rigidBody.bodyType = RigidbodyType2D.Static;
            SceneController.LoadGameOverScene();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collidesWithPlayer = false;
            btnPressTimer = 0;
        }
    }
}