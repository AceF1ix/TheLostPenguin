using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRising : MonoBehaviour
{
    private float timer = 0f;
    private float lvl_duration = 30;
    private float max_water_lvl = 150f;
    private Vector3 water_start_pos = new Vector3(0, 0, 0);
    public bool is_max_duration = false;
    public bool reset_pos = false;
    



    public void Start()
    {
        transform.position = water_start_pos;

        if(is_max_duration == false && reset_pos == false)
        {
            StartCoroutine(Rise());
        }
    }

    private IEnumerator Rise()
    {
        Vector3 start_pos = transform.position;
        Vector3 max_size = new Vector3(0, max_water_lvl, 0);

        do { // We will do all of this while our timer is less than our timer duration. 

            //Water risings
            transform.position = Vector3.Lerp(start_pos, max_size, timer / lvl_duration);
            //Increment timer
            timer += Time.deltaTime;
            yield return null;

        }

        while (timer < lvl_duration && reset_pos == false);
        is_max_duration = true;
    }

    public void restart_pos()
    {
        transform.position = water_start_pos;
        reset_pos = true;
    }

}
