using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject GC;
    private static int num;
    private void Start()
    {
        GC = GameObject.Find("GameController");
        Debug.Log(GC);
    }

    public void Clicked(int num)
    {
        GC.GetComponent<GameController>().LoadLevel(num);
    }
}
