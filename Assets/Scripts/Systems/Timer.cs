using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private GameManager _g;
    
    //Timer Start Value in seconds
    [SerializeField] private int _timerStart;
    public float timerCurrent;
    private TMP_Text displayTimer;
    private bool pause;
    
    // Update is called once per frame
    private void Start()
    {
        _g = GameManager.current;
        displayTimer = this.GetComponent<TMP_Text>();
        timerCurrent = _timerStart;

        _g.onTimerPause += PauseTimer;
        _g.onTimerPlay += PlayTimer;

        _g.onRunStart += ResetTimer;
    }

    private void FixedUpdate()
    {
        while (!pause)
        {
            timerCurrent -= Time.deltaTime;
        
            if (timerCurrent <= 0)
            {
                TimerComplete();
                displayTimer.text = "00:00";
                pause = true;
            }
            else
            {
                double mins = timerCurrent / 60;
                double secs = timerCurrent % 60;
                mins = Math.Floor(mins);
                secs = Math.Floor(secs);
                displayTimer.text = mins + ":" + secs;
            }
        }
    }

    public void ResetTimer()
    {
        timerCurrent = _timerStart;
    }

    private void PauseTimer()
    {
        pause = true;
    }

    private void PlayTimer()
    {
        pause = false;
    }

    private void TimerComplete()
    {
        _g.TimerComplete();
    }
}
