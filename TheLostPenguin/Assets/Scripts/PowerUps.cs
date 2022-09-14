using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PowerUps : MonoBehaviour
{
    private float jumpTimer = 0f;
    private float speedTimer = 0f;
    private float gameTimer = 0f;
    private float Jump_Boost_Duration = 10f;
    private float Sprint_Boost_Duration = 10f;
    private float Jump_Boost_Multiplier = 1.5f;
    private float Sprint_Boost_Multiplier = 1.3f;
    public bool winState = false;
    public ThirdPersonMovement thirdPersonMovement;
    public TextMeshProUGUI jumpText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winTimeText;
    public TextMeshProUGUI bestTimeText;
    public RawImage jumpImg;
    public RawImage speedImg;
    public Texture jumpNoActive;
    public Texture jumpYesActive;
    public Texture speedNoActive;
    public Texture speedYesActive;
    public GameObject winCanvas;

    public IEnumerator Player_Jump_Boost()
    {
        // Do something while the timer is on

        thirdPersonMovement.jumpForce *= Jump_Boost_Multiplier;
        jumpText.enabled = true;
        jumpImg.texture = jumpYesActive;

        do {
            jumpTimer += Time.deltaTime;
            jumpText.text = (Jump_Boost_Duration - Mathf.Abs(jumpTimer)).ToString("F0");
            yield return null;
        }


        while (jumpTimer < Jump_Boost_Duration);
        jumpText.enabled = false;
        jumpImg.texture = jumpNoActive;
        thirdPersonMovement.jumpForce = 15f;
    }

    public IEnumerator Player_Sprint_Boost()
    {
        thirdPersonMovement.speed *= Sprint_Boost_Multiplier;
        speedText.enabled = true;
        speedImg.texture = speedYesActive;

        do {
            speedTimer += Time.deltaTime;
            speedText.text = (10 - Mathf.Abs(speedTimer)).ToString("F0");
            yield return null;
        }

        while (speedTimer < Sprint_Boost_Duration);
        speedText.enabled = false;
        speedImg.texture = speedNoActive;
        thirdPersonMovement.speed = 30f;
    }

    public IEnumerator Timer_Counter()
    {
        timerText.enabled = true;
        Scene scene = SceneManager.GetActiveScene();

        do {
            gameTimer += Time.deltaTime;
            timerText.text = gameTimer.ToString("F2");
            yield return null;
        }

        while (winState == false);
        if(gameTimer < PlayerPrefs.GetFloat("bestrun") || PlayerPrefs.GetFloat("bestrun") == 0)
        {
            PlayerPrefs.SetFloat("bestrun", gameTimer);
        }
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        winTimeText.text = "Time: " + gameTimer.ToString("F2");
        bestTimeText.text = "Best Run: " + PlayerPrefs.GetFloat("bestrun").ToString("F2");
        winCanvas.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "winPosition")
        {
            winState = true;
        }
    }
}
