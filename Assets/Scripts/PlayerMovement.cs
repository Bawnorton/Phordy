using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private const float SPEED = 5f;

    private GameObject floor;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        floor = GameObject.Find("Floor");
    }

    private void Update() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var velocity = rb.velocity;

        rb.velocity = new Vector3(horizontal * SPEED, velocity.y, vertical * SPEED);
        if (Input.GetButtonDown("Jump")) rb.velocity = new Vector3(velocity.x, SPEED, velocity.z);

        var floorPos = floor.transform.position;
        floor.transform.position = new Vector3(floorPos.x, floorPos.y, rb.position.z);
    }
}