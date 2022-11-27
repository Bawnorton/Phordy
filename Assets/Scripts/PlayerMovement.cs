using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private float SPEED = 5f;

    private Rigidbody rb;

    private bool canJump;

    [Header("Player Stats")]
    public int coins = 0;
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform groundCheck;

    private Rigidbody rb;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        canJump = true;
    }

    private void FixedUpdate() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var velocity = rb.velocity;
        
        rb.velocity = new Vector3(horizontal * SPEED, velocity.y, vertical * SPEED);
        if (Input.GetButton("Jump") && canJump)
        {
            canJump = false;
            rb.velocity = new Vector3(velocity.x, SPEED, velocity.z);
        }

        var floorPos = GameController.floor.transform.position;
        GameController.floor.transform.position = new Vector3(floorPos.x, floorPos.y, rb.position.z);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("MonsterLayer"))
            Death();

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {
            Destroy(collision.collider.gameObject);
            coins++;
        }
        else
            canJump = true;
    }

    private void Death()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<UIController>().playerDeath();
    }
}