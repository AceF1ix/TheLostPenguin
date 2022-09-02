using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private float timer = 0f;
    private float Jump_Boost_Duration = 10f;
    private float Sprint_Boost_Duration = 10f;
    private float Jump_Boost_Multiplier = 1.5f;
    private float Sprint_Boost_Multiplier = 1.3f;
    public ThirdPersonMovement thirdPersonMovement;

    public IEnumerator Player_Jump_Boost()
    {
        // Do something while the timer is on

        thirdPersonMovement.jumpForce *= Jump_Boost_Multiplier;

        do {

            timer += Time.deltaTime;
            yield return null;
        }


        while (timer < Jump_Boost_Duration);
        thirdPersonMovement.jumpForce = 15f;

    }

    public IEnumerator Player_Sprint_Boost()
    {
        thirdPersonMovement.speed *= Sprint_Boost_Multiplier;

        do {
            
            timer += Time.deltaTime;
            yield return null;
        }

        while (timer < Sprint_Boost_Duration);
        thirdPersonMovement.speed = 30f;
    }

}
