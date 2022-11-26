using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private const float SPEED = 4f;

    // private GameObject floor;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = 1;
        var velocity = rb.velocity;

        rb.velocity = new Vector3(horizontal * SPEED, velocity.y, 0);
    }
}
