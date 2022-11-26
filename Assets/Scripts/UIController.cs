using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private PlayerMovement player;

    private bool menuActive;

    //Must be assigned in unity editor
    public Canvas menu;
    public TMP_Text scoreText;
    

    void Start()
    {
        menuActive = false;
        menu.gameObject.SetActive(false);
        player = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        scoreText.text = "Score: " + player.coins * 10;

        if (Input.GetButtonDown("Cancel"))
        {
            menu.gameObject.SetActive(menuActive = !menuActive);
        }
    }

    //Button methods
    public void Quit()
    {
        Application.Quit();
    }
}
