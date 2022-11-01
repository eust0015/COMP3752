using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace UI.Utilities
{
    public class WaitUtility : Utility
    {
        [SerializeField] private int secondsToWait;
        [SerializeField] private bool realTime;
        [SerializeField] private UnityEvent afterWaitEvent;

        public int SecondsToWait
        {
            get => secondsToWait;
            set => secondsToWait = value;
        }

        public bool RealTime
        {
            get => realTime;
            set => realTime = value;
        }

        public UnityEvent AfterWaitEvent
        {
            get => afterWaitEvent;
            set => afterWaitEvent = value;
        }
        
        public override void DoUtility()
        {
            StartCoroutine(WaitCoroutine());
        }
        
        IEnumerator WaitCoroutine()
        {
            if (RealTime)
                yield return new WaitForSecondsRealtime(SecondsToWait);
            else
                yield return new WaitForSeconds(SecondsToWait);
            AfterWaitEvent.Invoke();
        }
    }
}