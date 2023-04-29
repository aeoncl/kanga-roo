using System;
using UnityEngine;

public class EggController : MonoBehaviour
{
    [SerializeField] private float jumpPower = 10;
    
    private Rigidbody2D rigidBody;
    private Vector2 direction;
    private bool collidesWithPlayer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (collidesWithPlayer && Input.GetKeyDown(KeyCode.E))
        {
            var horizontalAxis = Input.GetAxis("Horizontal");
            direction = new Vector2(horizontalAxis, 1).normalized;
            
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
            rigidBody.AddForce(direction * jumpPower, ForceMode2D.Impulse);
            rigidBody.AddTorque(10f);
            collidesWithPlayer = false;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collidesWithPlayer = true;
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
            rigidBody.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collidesWithPlayer = false;
        }    
    }
}