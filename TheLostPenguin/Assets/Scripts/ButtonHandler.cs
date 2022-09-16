using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonHandler : MonoBehaviour
{
    public Escaper escaper;
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
    }

    public void OnClickResume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        escaper.optionsMenu.gameObject.SetActive(false);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Path7");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("Main");
    }
}
