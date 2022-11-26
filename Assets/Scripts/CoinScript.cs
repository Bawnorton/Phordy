using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 1f;
    private float startZ = 0;
    [SerializeField]
    private float width = 0;

    Renderer rend;

    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        startZ = gameObject.transform.position.z;
        rend = gameObject.GetComponent<Renderer>();
    }

    
    void Update()
    {
        if ((Mathf.Abs(player.transform.position.z - transform.position.z) > width))
            rend.enabled = false;
        else
            rend.enabled = true;


        transform.Rotate(new Vector3(0,spinSpeed,0));
    }
}
