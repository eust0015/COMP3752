using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current { get; private set; }

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(this);
        }
        else
        {
            current = this;
        }
    }

    public event Action onRoomStart;
    protected virtual void RoomStart()
    {
        onRoomStart?.Invoke();
    }
    
    public event Action onRoomEnd;
    protected virtual void RoomEnd()
    {
        onRoomEnd?.Invoke();
    }
    
    public event Action onRunStart;
    protected virtual void RunStart()
    {
        onRunStart?.Invoke();
    }
    
    public event Action onRunEnd;
    protected virtual void RunEnd()
    {
        onRunEnd?.Invoke();
    }

    public event Action onTimerPause;
    protected virtual void OnOnTimerPause()
    {
        onTimerPause?.Invoke();
    }

    public event Action onTimerPlay;
    protected virtual void OnOnTimerPlay()
    {
        onTimerPlay?.Invoke();
    }
    
    public event Action onTimerComplete;

    protected virtual void TimerComplete()
    {
        onTimerComplete?.Invoke();
    }


    
}
