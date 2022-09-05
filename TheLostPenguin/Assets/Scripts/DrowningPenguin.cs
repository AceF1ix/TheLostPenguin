using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrowningPenguin : MonoBehaviour
{

    private float timer = 0f;
    private float drowning_duration = 15f;
    private Vector3 end_drowning = new Vector3(86.93f, -8.8f, 167.47f);
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Drowing_Penguin());
    }

    private IEnumerator Drowing_Penguin()
    {
        initialPosition = transform.position;
        do 
        {

            transform.position = Vector3.Lerp(initialPosition, end_drowning, timer / drowning_duration);

            timer += Time.deltaTime;
            yield return null;
        }


        while (timer < drowning_duration);
    }

}
