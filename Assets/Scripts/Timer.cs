using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timerGame;
    public Text timer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerGame += Time.deltaTime;
        UpdateTimer(timerGame);
    }

    void UpdateTimer(float time)
    {
        int minutes = Mathf.FloorToInt(timerGame / 60f);
        int seconds = Mathf.FloorToInt(timerGame - (minutes * 60));

        timer = gameObject.GetComponent<Text>();
        timer.text = string.Format("{0,0}:{1:00}", minutes, seconds);
    }
}
