using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levelup : MonoBehaviour
{
    public Text leveluptext;
    public int level = 1 ;
    public Timer timer;
    public GameObject leveluptime;
    public bool levelup = true;


    // Start is called before the first frame update
    void Start()
    {
        leveluptext = GetComponent<Text>();
        leveluptext.text = "Level " + level;
        Timer timer = leveluptime.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.timerGame > 30 && levelup)
        {
            level = 2;
            leveluptext.text = "Level " + level;
            
        }

        if (timer.timerGame > 60 && levelup)
        {
            level = 3;
            leveluptext.text = "Level " + level;
        }
    }
}
