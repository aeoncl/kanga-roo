using System;
using UnityEngine;

public class EggController : MonoBehaviour
{
    [SerializeField] private float jumpPower = 10;

    private Rigidbody2D rigidBody;
    private Vector2 direction;
    private bool collidesWithPlayer;

    private bool doUpdate = false;

    private bool isLaunched = false;

    private float btnPressTimer;

    public float btnPressTimeout = 0.5f;
    public float btnDoubleTapTimeout = 0.5f;
    public float torque = 10f;

    public float windMultiplier = 10;

    private bool isInWind = false;

    public Animator playerAnimator;

    public bool IsVisible {get;set;}

    public AudioSource audiosource;
    public AudioClip[] boxHitsSounds;
    public AudioClip boxDestroyedSound;

    public AudioClip truckSound;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Time.timeScale = 0f;
        this.IsVisible = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isLaunched)
            {
                Time.timeScale = 1f;
                rigidBody.bodyType = RigidbodyType2D.Dynamic;
                rigidBody.AddForce(new Vector2(0.5f, 0.5f) * jumpPower, ForceMode2D.Impulse);
                rigidBody.AddTorque(this.torque);
                isLaunched = true;
                this.audiosource.PlayOneShot(truckSound);

            }

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
            doUpdate = false;
            this.audiosource.PlayOneShot(this.boxHitsSounds[this.getHitSoundIndex()]);
        }

        if(this.isInWind) {

                Debug.Log("Velocity x: " + rigidBody.velocity.x);

                if(rigidBody.velocity.x >= 0) {
                    rigidBody.velocity = Vector2.zero;
                    rigidBody.angularVelocity = 0f;
                    rigidBody.gravityScale = 0;
                    //rigidBody.AddTorque(this.torque);
                    rigidBody.AddForce((Vector2.right) * windMultiplier);
                } else {
                    rigidBody.AddForce((Vector2.right) / 4, ForceMode2D.Impulse);
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collidesWithPlayer = true;
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            LevelProgressSingleton.Instance.destroyedPackages++;
            this.audiosource.PlayOneShot(this.boxDestroyedSound);
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
            rigidBody.bodyType = RigidbodyType2D.Static;
            SceneController.LoadGameOverScene();
        }

        if (other.gameObject.CompareTag("Wind")) {
            this.isInWind = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collidesWithPlayer = false;
            btnPressTimer = 0;
        }

        
        if (other.gameObject.CompareTag("Wind")) {
            this.isInWind = false;
            rigidBody.gravityScale = 1;
            rigidBody.velocity -= new Vector2(2,2);
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("BOULE DEBUG: Egg Became invisible");
        IsVisible = false;
    }


    void OnBecameVisible()
    {
       Debug.Log("BOULE DEBUG: Egg Became visible");
        IsVisible = true;
    }

    private int getHitSoundIndex() {
        System.Random r = new System.Random();
        int rInt = r.Next(0, this.boxHitsSounds.Length);
        return rInt;
    }
}