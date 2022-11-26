using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private const float SPEED = 5f;

    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var velocity = rb.velocity;
        
        if (Input.GetButtonDown("Jump")) rb.velocity = new Vector3(velocity.x, SPEED, velocity.z);

        rb.velocity = new Vector3(horizontal * SPEED, velocity.y, vertical * SPEED);

        var floorPos = GameController.floor.transform.position;
        GameController.floor.transform.position = new Vector3(floorPos.x, floorPos.y, rb.position.z);
    }
}