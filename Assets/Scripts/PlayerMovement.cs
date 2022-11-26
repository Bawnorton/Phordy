using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    private const float SPEED = 5f;

    private Rigidbody rb;
    private GameObject floor;
    private bool canJump;

    [Header("Player Stats")]
    public int coins = 0;


    private void Start() {
        rb = GetComponent<Rigidbody>();
        floor = GameObject.Find("Floor");
        canJump = true;
    }

    private void Update() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var velocity = rb.velocity;
        
        rb.velocity = new Vector3(horizontal * SPEED, velocity.y, vertical * SPEED);
        if (Input.GetButtonDown("Jump") && canJump)
        {
            canJump = false;
            rb.velocity = new Vector3(velocity.x, SPEED, velocity.z);
        }

        var floorPos = GameController.floor.transform.position;
        GameController.floor.transform.position = new Vector3(floorPos.x, floorPos.y, rb.position.z);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.gameObject.layer != LayerMask.NameToLayer("Coin"))
            canJump = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {
            collision.collider.gameObject.SetActive(false);
            coins++;
        }
        else
            canJump = true;
    }


}