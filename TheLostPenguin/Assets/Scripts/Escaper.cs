using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Escaper : MonoBehaviour
{
    public GameObject optionsMenu;
    public bool gameIsPaused;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        if(scene.name == "Main")
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                gameIsPaused = !gameIsPaused;
                PauseGame();
                optionsMenu.gameObject.SetActive(!optionsMenu.gameObject.activeSelf);
            }

            if(!optionsMenu.gameObject.activeSelf && gameIsPaused == true)
            {
                gameIsPaused = false;
            }
        }
    }

    void PauseGame()
    {
        if(gameIsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0; 
        } 
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
