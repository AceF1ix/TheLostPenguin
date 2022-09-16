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
    public bool jumpBoostActive = false;
    public bool speedBoostActive = false;
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

    void Update()
    {
        if (jumpBoostActive == true)
        {
            if (jumpTimer < Jump_Boost_Duration)
            {
                jumpTimer += Time.deltaTime;
                jumpText.text = (10 - Mathf.Abs(jumpTimer)).ToString("F0");
            } 
            else
            {
                jumpBoostActive = false;
                jumpTimer = 0f;
                jumpText.enabled = false;
                jumpImg.texture = jumpNoActive;
                thirdPersonMovement.jumpForce = 16f;
            }
        }

        if (speedBoostActive == true)
        {
            if (speedTimer < Sprint_Boost_Duration)
            {
                speedTimer += Time.deltaTime;
                speedText.text = (10 - Mathf.Abs(speedTimer)).ToString("F0");
            }
            else
            {
                speedBoostActive = false;
                speedTimer = 0;
                speedText.enabled = false;
                speedImg.texture = speedNoActive;
                thirdPersonMovement.speed = 30f;
            }
        }
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

        if(other.gameObject.tag == "JumpBoostGem")
        {
            Destroy(other.gameObject);
            jumpBoostActive = true;
            thirdPersonMovement.jumpForce *= Jump_Boost_Multiplier;
            jumpText.enabled = true;
            jumpImg.texture = jumpYesActive;
        }

        if(other.gameObject.tag == "SprintBoostGem")
        {
            Destroy(other.gameObject);
            speedBoostActive = true;
            thirdPersonMovement.speed *= Sprint_Boost_Multiplier;
            speedText.enabled = true;
            speedImg.texture = speedYesActive;
        }
    }
}
