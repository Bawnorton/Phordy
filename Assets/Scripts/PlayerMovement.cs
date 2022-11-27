using System;
using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] public int coins;
    private Vector3 input;

    private void Update() {
        GatherInput();
        if(Input.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
        }
    }

    private void FixedUpdate() {
        Move();
    }

    private void GatherInput() {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Move() {
        Transform t = transform;
        Vector3 result = t.position + speed * Time.deltaTime * (t.forward * input.z + t.right * input.x);
        rb.MovePosition(result);
    }
    
    private void Jump() {
        rb.velocity = Vector3.up * jumpForce;
    }
    
    private bool IsGrounded() { 
        Transform t = transform;
        Vector3 scale = t.localScale;
        Vector3 position = t.position;
        Vector3 right = t.right;
        Vector3 forward = t.forward;
        return Physics.Raycast(position + scale.x * right, Vector3.down, scale.y * 0.5f) ||
               Physics.Raycast(position - scale.x * right, Vector3.down, scale.y * 0.5f) ||
               Physics.Raycast(position + scale.z * forward, Vector3.down, scale.y * 0.5f) ||
               Physics.Raycast(position - scale.z * forward, Vector3.down, scale.y * 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {
            Destroy(other.gameObject);
            coins++;
        }
    }
}