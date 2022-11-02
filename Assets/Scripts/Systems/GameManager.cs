using System;
using System.Collections;
using System.Collections.Generic;
using UI.Items;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current { get; private set; }

    private void Awake()
    {
        
        if (current != null && current != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            current = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public event Action onRoomStart;
    public virtual void RoomStart()
    {
        Debug.Log("Invoked -> Room Start");
        onRoomStart?.Invoke();
    }
    
    public event Action onRoomEnd;
    public virtual void RoomEnd()
    {
        Debug.Log("Invoked -> Room End");
        onRoomEnd?.Invoke();
    }
    
    public event Action onRunStart;
    public virtual void RunStart()
    {
        Debug.Log("Invoked -> Run Start");
        onRunStart?.Invoke();
    }
    
    public event Action onRunEnd;
    public virtual void RunEnd()
    {
        Debug.Log("Invoked -> Run End");
        onRunEnd?.Invoke();
    }

    public event Action onTimerPause;
    public virtual void OnOnTimerPause()
    {
        Debug.Log("Invoked -> Timer Pause");
        onTimerPause?.Invoke();
    }

    public event Action onTimerPlay;
    public virtual void OnOnTimerPlay()
    {
        Debug.Log("Invoked -> Timer Play");
        onTimerPlay?.Invoke();
    }
    
    public event Action onTimerComplete;

    public virtual void TimerComplete()
    {
        Debug.Log("Invoked -> Timer Complete");
        onTimerComplete?.Invoke();
    }

    public event Action<PlayerItem> onItemObtained;

    public virtual void ItemObtained(PlayerItem it)
    {
        //Debug.Log("Invoked -> Item Obtained - " + it.itemName);
        onItemObtained?.Invoke(it);
    }


}
