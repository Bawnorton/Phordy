using System;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    private GameObject self;
    private GameObject player;
    private Renderer rend;
    private float width;
    private float relZ;
    
    public float startZ;
    private void Start() {
        player = GameObject.Find("Player");
        rend = GetComponent<Renderer>();
        self = gameObject;
        width = self.transform.localScale.z;
        relZ = self.transform.position.z;
    }

    private void Update() {
        Vector3 scale = self.transform.localScale;
        Vector3 pos = self.transform.position;
        Vector3 playerPos = player.transform.position;
        self.transform.localScale = new Vector3(
            scale.x,
            scale.y,
            Math.Min(Math.Max(width - Math.Abs(playerPos.z - startZ), 0), width));
        rend.enabled = scale.z > 0;
        self.transform.position = new Vector3(
            pos.x,
            pos.y,
            playerPos.z / 2 + relZ);
    }
}
