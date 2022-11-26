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
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text deathText;

    private bool canToggle = true;
    void Start()
    {
        menuActive = false;
        menu.gameObject.SetActive(false);
        player = gameObject.GetComponent<PlayerMovement>();
        deathText.gameObject.SetActive(false);
        canToggle = true;
    }

    void Update()
    {
        scoreText.text = "Score: " + player.coins * 10;

        if (Input.GetButtonDown("Cancel") && canToggle)
        {
            menu.gameObject.SetActive(menuActive = !menuActive);
        }
    }

    public void playerDeath()
    {
        menu.gameObject.SetActive(true);
        deathText.gameObject.SetActive(true);
        canToggle = false;
    }

    //Button methods
    public void Quit()
    {
        Application.Quit();
    }
}
