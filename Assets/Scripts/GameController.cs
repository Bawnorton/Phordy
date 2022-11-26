using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
    void Start() {
        int numCubes = 10;
        for (int i = 0; i < numCubes; i++) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            while (true) {
                Vector3 pos = new Vector3(Random.Range(-50, 50), 0.5f, Random.Range(-50, 50));
                cube.transform.position = pos;
                if (!Physics.CheckBox(pos, new Vector3(1, 1, 0.5f))) {
                    break;
                }
            }
            cube.transform.localScale = new Vector3(2, 2, 0.45f);
            cube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
            cube.AddComponent<RenderBackface>();
        }
    }
    
    void Update() {
    }
}