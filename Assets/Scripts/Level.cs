using UnityEngine;

// levelString is a string of 0s and 1s, with 0s being empty space and 1s being platforms
// first characters are a|b|c| where a is an integer representing the width, b, height, and c, length of the level
// a b and c can be any integer from 0 to 100
// then the rest of the string is the level data formatted such that:
// each platform object is represented by a 1 followed by "[x,y,z,length,offset]" where x, y, and z are integers representing the position of the platform
// and where length is the length of the platform and offset is the offset of the platform from the origin
// followed by a . and then the next platform object
// example a 3x3x3 cube with a 1x1x1 platform in the center would be represented as "3|3|3|1[1,1,1,1,0]." 
// example a 2x2x2 cube of platforms would be represented as "2|2|2|1[0,0,0,1,0].1[1,0,0,1,0].1[0,1,0,1,0].1[1,1,0,1,0].1[0,0,1,1,0].1[1,0,1,1,0].1[0,1,1,1,0].1[1,1,1,1,0]."
// the last character is a period, which is ignored
public class Level {
    // 3d array of tiles
    private GameObject[] platforms;
    
    private int levelWidth;
    private int levelHeight;
    private int levelLength;
    
    private readonly Material platformMaterial = GameObject.Find("Platform").GetComponent<Renderer>().material;

    private void Init(int w, int h, int l, string levelString) {
        levelWidth = w;
        levelHeight = h;
        levelLength = l;
        platforms = new GameObject[levelWidth * levelHeight * levelLength];
        // get the level data
        string[] levelData = levelString.Split('.');
        for (int i = 0; i < levelData.Length; i++) {
            // get the platform data
            string[] platformData = levelData[i].Split('[');
            if(platformData.Length < 2) continue;
                // get the position data
            string[] positionData = platformData[1].Split(',');
            // get the position
            int x = int.Parse(positionData[0]);
            int y = int.Parse(positionData[1]);
            int z = int.Parse(positionData[2]);
            // get the length
            int length = int.Parse(positionData[3]);
            // get the offset
            int offset = int.Parse(positionData[4].Substring(0, positionData[4].Length - 1));
            // create the platform
            platforms[i] = CreatePlatform(x, y, z, length, offset);
        }
    }
    public Level(string levelString) {
        string[] levelDimensions = levelString.Split('|');
        int w = int.Parse(levelDimensions[0]);
        int h = int.Parse(levelDimensions[1]);
        int l = int.Parse(levelDimensions[2]);
        Init(w, h, l, levelDimensions[3]);
    }
    
    public Level(int w, int h, int l, string levelString) {
        Init(w, h, l, levelString);
    }

    private GameObject CreatePlatform(int x, int y, int z, int length, int offset) {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = new Vector3(x, y, z);
        cube.transform.localScale = new Vector3(1, 1, length);
        cube.AddComponent<PlatformController>();
        cube.GetComponent<PlatformController>().startZ = offset;
        cube.GetComponent<Renderer>().material = platformMaterial;
        cube.SetActive(false);
        return cube;
    }

    public void GenerateLevel() {
        foreach (var platform in platforms) {
            if(platform != null) platform.SetActive(true);
        }
    }
    
    public Vector3 GetLevelSize() {
        return new Vector3(levelWidth, levelHeight, levelLength);
    }
}