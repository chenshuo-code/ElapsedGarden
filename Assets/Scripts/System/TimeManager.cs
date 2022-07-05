using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For all time system in game
/// </summary>
public class TimeManager : MonoBehaviour
{
    [HideInInspector]public float GameTime; //Total game time from begin of the game

    public float TimerInterval; //Time scale for time counter
    
    public event CallBack EventTimePass; //Event call in each time count

    private float timer;
    public void Init()
    {
        GameTime = 0;
        timer = 0;
    }
    private void Update()
    {
        TimeCounter();
    }
    private void TimeCounter()
    {
        timer += Time.deltaTime;
        if (timer >= TimerInterval)
        {
            timer = 0;
            GameTime++;
            if (EventTimePass != null) EventTimePass();
        }
    }

}
