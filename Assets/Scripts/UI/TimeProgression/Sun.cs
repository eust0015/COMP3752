using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI.TimeProgression
{
    public class Sun : MonoBehaviour
    {
        private GameManager _g;
        [SerializeField] private Animator sunAnimator;
        private static readonly int Night = Animator.StringToHash("night");
        private static readonly int Moving = Animator.StringToHash("moving");

        public Animator SunAnimator
        {
            get => sunAnimator;
            private set => sunAnimator = value;
        }

        // private void Update()
        // {
        //     if(Input.GetKeyDown(KeyCode.N))
        //         SunAnimator.SetBool(Night, true);
        //     else if(Input.GetKeyDown(KeyCode.D))
        //         SunAnimator.SetBool(Night, false);
        //     else if(Input.GetKeyDown(KeyCode.M))
        //         SunAnimator.SetBool(Moving, true);
        //     else if(Input.GetKeyDown(KeyCode.S))
        //         SunAnimator.SetBool(Moving, false);
        // }

        private void Start()
        {
            _g = GameManager.current;
            _g.onRunStart += OnRunStart;
            _g.onTimerPlay += OnTimerPlay;
            _g.onTimerPause += OnTimerPause;
            //_g.onTimerComplete += OnTimerComplete;
            OnRunStart();
        }

        private void OnRunStart()
        {
            SunAnimator.SetBool(Night, false);
            SunAnimator.SetBool(Moving, true);
            StartCoroutine(DayCoroutine());
        }

        IEnumerator DayCoroutine()
        {
            yield return new WaitForSeconds(180);
            OnTimerComplete();
        }
        
        private void OnTimerPlay()
        {
            
        }

        private void OnTimerPause()
        {

        }

        private void OnTimerComplete()
        {
            SunAnimator.SetBool(Night, true);
            SunAnimator.SetBool(Moving, false);
        }

        private void OnDestroy()
        {
            _g.onRunStart -= OnRunStart;
            _g.onTimerPlay -= OnTimerPlay;
            _g.onTimerPause -= OnTimerPause;
            _g.onTimerComplete -= OnTimerComplete;
        }
    }
}