using UnityEngine;

public class EggController : MonoBehaviour
{
    [SerializeField] private float jumpPower = 10;
    
    private Rigidbody2D rigidBody;
    private Vector2 direction;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var horizontalAxis = Input.GetAxis("Horizontal");
        direction = new Vector2(horizontalAxis, 1).normalized;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
            rigidBody.AddForce(direction * jumpPower, ForceMode2D.Impulse);
            rigidBody.AddTorque(10f);
        }
    }
}