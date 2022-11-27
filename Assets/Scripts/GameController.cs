using System;
using UnityEngine;

public class GameController : MonoBehaviour {
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject tutorial;
    private PauseMenu pauseMenuScript;
    
    private int maxZ;
    private int minZ;
    
    private Level.Builder lvl0;
    private Level.Builder lvl1;
    private Level.Builder lvl2;
    private static Level.Builder[] levels;

    public static Level currentLevel;
    private void Start() {
        try {
            GameObject c = GameObject.FindGameObjectWithTag("MainCamera");
            c.GetComponent<SimulateColorBlindness>().SetMode(SaveData.instance.colourCorrection);
        }
        catch (NullReferenceException) { }

        int loadedLevel = LevelSelectMenu.num;
        pauseMenuScript = pauseMenu.GetComponent<PauseMenu>();
        ScoreController.score = 0;
        ScoreController.deaths = 0;
        
        maxZ = (int)GameObject.Find("Backwall").transform.position.z;
        minZ = (int)GameObject.Find("Frontwall").transform.position.z;

        int levelWidth = Mathf.Abs(maxZ) + Mathf.Abs(minZ);

        lvl0 = new Level.Builder(levelWidth, 5, 40)
            .P(5, 0, 0, 1, 0, 1) // example cube
            .P(10, 0, 0, 10, 0, 1) // example wide cube
            .P(15, 0, 0, 5, 2, 1) // example offset cube
            .P(15, 3, 0, 5, -2, 1) // example offset cube
            .P(20, 0, 4, levelWidth + 4, -8, 1) // example wall
            .P(20, 1, 0, levelWidth, 6, 1) // example wall
            .P(21, 0, 0, levelWidth, 0, 2) // example wall
            .P(21, 1, 0, levelWidth, 0, 2) // example wall
            .P(21, 2, 0, levelWidth, 0, 2) // example wall
            .C(23, 1, levelWidth / 2)
            .S(27, 0, 0, levelWidth / 2) // spike jump
            .P(33, 0, 0, 5, 2, 1)
            .P(35, 1, 1, 6, 3, 1)
            .C(35, 3, 6)
            .C(37, 2, 1) // main coin
            .S(33, 0, 0, levelWidth) // spike floor
            .S(34, 0, 0, levelWidth)
            .S(35, 0, 0, levelWidth)
            .S(36, 0, 0, levelWidth)
            .S(37, 0, 0, levelWidth)
            .S(38, 0, 0, levelWidth);
        


        lvl1 = new Level.Builder(levelWidth, 5, 30)
           .S(1, 0, 0, levelWidth).S(2, 0, 0, levelWidth).S(3, 0, 0, levelWidth)
           .S(4, 0, 0, levelWidth).S(5, 0, 0, levelWidth).S(6, 0, 0, levelWidth)
           .S(7, 0, 0, levelWidth).S(8, 0, 0, levelWidth).S(9, 0, 0, levelWidth)
           .S(10, 0, 0, levelWidth).S(11, 0, 0, levelWidth).S(12, 0, 0, levelWidth)
           .S(13, 0, 0, levelWidth).S(14, 0, 0, levelWidth).S(15, 0, 0, levelWidth)
           .S(16, 0, 0, levelWidth).S(17, 0, 0, levelWidth).S(18, 0, 0, levelWidth)
           .S(19, 0, 0, levelWidth).S(20, 0, 0, levelWidth).S(21, 0, 0, levelWidth)
           .S(22, 0, 0, levelWidth).S(23, 0, 0, levelWidth).S(24, 0, 0, levelWidth)
           .S(25, 0, 0, levelWidth).S(26, 0, 0, levelWidth).S(27, 0, 0, levelWidth)
           .S(28, 0, 0, levelWidth).S(29, 0, 0, levelWidth).S(30, 0, 0, levelWidth)
           .P(0, 0, 0, 2, 0, 1).P(2, 1, 0, 3, 1, 1).P(3, 1, 0, 4, 2, 1)
           .P(5, 2, 1, 4, 4, 1).P(5, 1, 1, 10, -5, 1).P(11, 1, 0, 6, 4, 1)
           .P(13, 2, 0, 3, 0, 1).P(14, 2, 0, 3, 2, 1).P(15, 2, 0, 3, -2, 1)
           .P(13, 4, 0, 3, 0, 1).P(14, 4, 0, 3, 2, 1).P(18, 2, -1, 6, -3, 1)
           .P(25, 0, 0, 2, 0, 1).P(27, 1, 0, 3, 1, 1).P(28, 2, -2, 5, -1, 1)
           .P(29, 1, 0, 10, 0, 2).P(29, 2, 0, 10, 0, 2).P(29, 3, 0, 10, 0, 2)

           .C(0, 2, 0).C(2, 3, 0).C(5, 4, 5).C(5, 3, -6).C(11, 3, 4).C(27, 5, 0);

        lvl2 = new Level.Builder(levelWidth, 10, 30)
            .S(1, 0, 0, levelWidth).S(2, 0, 0, levelWidth).S(3, 0, minZ + 2, 4).S(3, 0, maxZ - 1, 1).S(3, 0, maxZ - 2, 2)
           .S(4, 0, 0, levelWidth).S(5, 0, 0, levelWidth).S(6, 0, 0, levelWidth)
           .S(7, 0, 0, levelWidth).S(8, 0, 0, levelWidth).S(9, 0, 0, levelWidth)
           .S(10, 0, 0, levelWidth).S(11, 0, 0, levelWidth).S(12, 0, 0, levelWidth)
           .S(13, 0, 0, levelWidth).S(14, 0, 0, levelWidth).S(15, 0, 0, levelWidth)
           .S(16, 0, 0, levelWidth).S(17, 0, 0, levelWidth).S(18, 0, 0, levelWidth)
           .S(19, 0, 0, levelWidth).S(20, 0, 0, levelWidth).S(21, 0, 0, levelWidth)
           .S(22, 0, 0, levelWidth).S(23, 0, 0, levelWidth).S(24, 0, 0, levelWidth)
           .S(25, 0, 0, levelWidth).S(26, 0, 0, levelWidth).S(27, 0, 0, levelWidth)
           .S(28, 0, 0, levelWidth).S(29, 0, 0, levelWidth).S(30, 0, 0, levelWidth)
           .P(3, 0, 0, 4, 0, 1).P(6, 0, 0, 4, 0, 1).P(8, 1, -2, 4, -3, 1).P(8, 2, 1, 2, 1, 1)
           .P(8, 3, 2, 3, 4, 1).P(11, 1, -2, 3, -3, 1).P(6, 4, 3, 4, 4, 1).P(6, 5, 0, 3, 0, 1)
           .P(9, 5, 0, 5, 0, 1).P(11,6,3,3,4,1).P(11,7,1,3,1,1).P(13,6,-3,3,-3,1).P(13,6,1,3,0,1)
           .P(16,6,0,2,0,1).P(18,6,0,2,0,1).P(19,6,0,2,0,1).S(19,7,1,3).P(19,9,0,2,0,1)
           .P(20,6,0,2,0,1).S(20, 7, 1, 3).P(20,9,0,2,0,1).P(21,6,0,2,0,1).S(21,7,1,3)
           .P(21,9,0,2,0,1).P(22, 6, 0, 2, 0, 1).S(22, 7, 1, 3).P(22, 9, 0, 2, 0, 1)
           .P(22,6,-2,3,-3,1).P(29,0,0,levelWidth,0,2).P(29,1,0,levelWidth,0,2).P(29,2,0,levelWidth,0,2)
           .P(29, 3, 0, levelWidth, 0, 2).P(29, 4, 0, levelWidth, 0, 2).P(29, 5, 0, levelWidth, 0, 2)
           .P(29, 6, 0, levelWidth, 0, 2).P(29, 7, 4, levelWidth-4, 0, 2).P(29, 7, 4, levelWidth-4, 0, 2)
           .C(3, 1, 5).C(11,2,-3).C(7,7,-1).C(11,8,2).C(13,7,-3).C(18,7,0).C(29,9,9);

        levels = new []{lvl0, lvl1, lvl2};
        LoadLevel(loadedLevel);
        
        tutorial.SetActive(loadedLevel == 0);
    }

    private static void LoadLevel(int num) {
        currentLevel?.Destroy();
        Debug.Log("Loading level " + num);
        currentLevel = levels[num].Build();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (PauseMenu.isPaused) {
                pauseMenuScript.Back();
            }
            else {
                pauseMenuScript.Pause();
            }
        }
    }
}