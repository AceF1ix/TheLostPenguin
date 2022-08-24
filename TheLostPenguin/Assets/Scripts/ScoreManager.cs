using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int gem_counter;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        gem_counter = 0;
        scoreText.text = "Gems: " + gem_counter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    public void AddPoint(){
        gem_counter += 1;
        scoreText.text = "Gems: " + gem_counter.ToString();
    }
}
