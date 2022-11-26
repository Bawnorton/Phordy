using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private const float SPEED = 4f;

    // private GameObject floor;
    private Rigidbody rb;
    private GameObject Player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // floor = GameObject.Find("Floor");
        GameObject Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = 1;
        var vertical = 0;
        var velocity = rb.velocity;

        rb.velocity = new Vector3(horizontal * SPEED, velocity.y, vertical * SPEED);
        //if (Input.GetButtonDown("Jump")) rb.velocity = new Vector3(velocity.x, SPEED, velocity.z);

        var playerZ = Player.transform.position.z;
        var monsterPos = rb.transform.position;
        rb.transform.position = new Vector3(monsterPos.x,monsterPos.y,playerZ);

        // var floorPos = floor.transform.position;
        // floor.transform.position = new Vector3(floorPos.x, floorPos.y, rb.position.z);
    }
}
