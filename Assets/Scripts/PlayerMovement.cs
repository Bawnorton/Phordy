using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    [Header("Player Stats")]
    public int coins = 0;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform groundCheck;

    private Rigidbody rb;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var velocity = rb.velocity;
        
        rb.velocity = new Vector3(horizontal * speed, velocity.y, vertical * speed);
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.velocity = new Vector3(velocity.x, speed, velocity.z);
        }

        var floorPos = GameController.floor.transform.position;
        GameController.floor.transform.position = new Vector3(floorPos.x, floorPos.y, rb.position.z);
    }

    private bool IsGrounded() {
        Vector3 pos = groundCheck.position;
        Vector3 localScale = transform.localScale;
        pos.x += localScale.x / 2 - 0.1f;
        bool front = Physics.Raycast(pos, Vector3.down, 0.1f);
        pos.x -= localScale.x - 0.2f;
        bool back = Physics.Raycast(pos, Vector3.down, 0.1f);
        return front || back;
    }
}