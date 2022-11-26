using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {
    private const int MAX_X = 100;
    private const int MAX_Y = 6;
    public const int MAX_Z = 6;
    
    private float zScale = 0.49f;
    
    private Vector3 startPos = new (0, MAX_Y/2.0f, MAX_Z/2.0f);
    private Vector3 endPos = new (MAX_X - 1, MAX_Y/2.0f, MAX_Z/2.0f);
    private int[][][] maze;

    void Start() {
        // generate the maze
        // start at the start position
        // randomly choose a direction to go in
        // if the direction is valid, go in that direction
        // if the direction is not valid, choose a new direction
        // repeat until you reach the end position
        
        maze = new int[MAX_X][][];
        for (int i = 0; i < MAX_X; i++) {
            maze[i] = new int[MAX_Y][];
            for (int j = 0; j < MAX_Y; j++) {
                maze[i][j] = new int[MAX_Z];
                for (int k = 0; k < MAX_Z; k++) {
                    maze[i][j][k] = 0;
                }
            }
        }
        maze[(int)startPos.x][(int)startPos.y][(int)startPos.z] = 1;
        
        Debug.Log(endPos);
        Vector3 currPos = startPos;
        Stack<Vector3> stack = new Stack<Vector3>();
        stack.Push(currPos);
        while (currPos != endPos) {
            // get the valid directions
            List<Vector3> validDirs = GetValidDirs(currPos);
            if (validDirs.Count == 0) {
                // if there are no valid directions, go back to the last position
                currPos = stack.Pop();
            } else {
                // choose a random direction
                int randIndex = Random.Range(0, validDirs.Count);
                Vector3 randDir = validDirs[randIndex];
                // go in that direction
                currPos += randDir;
                stack.Push(currPos);
                maze[(int)currPos.x][(int)currPos.y][(int)currPos.z] = 1;
            }
        }
        // remove 75% of the platforms
        for (int i = 0; i < MAX_X; i++) {
            for (int j = 0; j < MAX_Y; j++) {
                for (int k = 0; k < MAX_Z; k++) {
                    if (maze[i][j][k] == 1 && Random.Range(0, 10) < 7.5) {
                        maze[i][j][k] = 0;
                    }
                }
            }
        }
        // instantiate the maze
        for (int i = 0; i < MAX_X; i++) {
            for (int j = 0; j < MAX_Y; j++) {
                for (int k = 0; k < MAX_Z; k++) {
                    if (maze[i][j][k] == 1) {
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        cube.transform.position = new Vector3(i, j, (k - MAX_Z / 2.0f) * zScale);
                        cube.transform.localScale = new Vector3(1, 1, 1 * zScale);
                        cube.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
                        cube.AddComponent<RenderBackface>();
                    }
                }
            }
        }
        
        // create two walls along the edges
        GameObject backWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        backWall.transform.rotation = Quaternion.Euler(180, 0, 0);
        backWall.transform.position = new Vector3(MAX_X / 2.0f, MAX_Y / 2.0f, -zScale / 2.0f * (MAX_Z + 2));
        backWall.transform.localScale = new Vector3(MAX_X, MAX_Y, 1);
        backWall.GetComponent<Renderer>().material.color = Color.black;
        
        GameObject frontWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        frontWall.transform.rotation = Quaternion.Euler(-180, 0, 0);
        frontWall.transform.position = new Vector3(MAX_X / 2.0f, MAX_Y / 2.0f, zScale / 2.0f * MAX_Z);
        frontWall.transform.localScale = new Vector3(MAX_X, MAX_Y, 1);
        frontWall.GetComponent<Renderer>().material.color = Color.black;

    }

    private List<Vector3> GetValidDirs(Vector3 currentPos) {
        List<Vector3> validDirs = new List<Vector3>();
        if (currentPos.y + 1 < MAX_Y && maze[(int)currentPos.x][(int)currentPos.y + 1][(int)currentPos.z] == 0) validDirs.Add(Vector3.up);
        if (currentPos.y - 1 >= 0 && maze[(int)currentPos.x][(int)currentPos.y - 1][(int)currentPos.z] == 0) validDirs.Add(Vector3.down);
        if (currentPos.x - 1 >= 0 && maze[(int)currentPos.x - 1][(int)currentPos.y][(int)currentPos.z] == 0) validDirs.Add(Vector3.left);
        if (currentPos.x + 1 < MAX_X && maze[(int)currentPos.x + 1][(int)currentPos.y][(int)currentPos.z] == 0) validDirs.Add(Vector3.right);
        if (currentPos.z + 1 < MAX_Z && maze[(int)currentPos.x][(int)currentPos.y][(int)currentPos.z + 1] == 0) validDirs.Add(Vector3.forward);
        if (currentPos.z - 1 >= 0 && maze[(int)currentPos.x][(int)currentPos.y][(int)currentPos.z - 1] == 0) validDirs.Add(Vector3.back);
        return validDirs;
    }



    void Update() {
        // check if player is at the end
        // if so, generate new path
    }
}