using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepWaterRise : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 0f;
    private float lvl_duration = 110;
    private Vector3 water_start_pos = new Vector3(0, 0, 0);
    public bool is_max_duration = false;
    public bool reset_pos = false;
    private Vector3 max_size = new Vector3(0, 320f, 0);
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < lvl_duration)
        {
            transform.position = Vector3.Lerp(water_start_pos, max_size, timer / lvl_duration);
            timer += Time.deltaTime;
        } 
        else 
        {
            transform.position = water_start_pos;
            timer = 0f;
        }
    }
}
