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
        [SerializeField] private int minutesInADay;
        private static readonly int Night = Animator.StringToHash("night");
        private static readonly int Moving = Animator.StringToHash("moving");

        public Animator SunAnimator
        {
            get => sunAnimator;
            private set => sunAnimator = value;
        }

        public int MinutesInADay
        {
            get => minutesInADay;
            set => minutesInADay = value;
        }

        private int SecondsInADay => minutesInADay * 60;
        private float AnimationSpeed => 1.0f / minutesInADay;

        private void Start()
        {
            _g = GameManager.current;
            _g.onRunStart += OnRunStart;
            _g.onTimerPlay += OnTimerPlay;
            _g.onTimerPause += OnTimerPause;
            //_g.onTimerComplete += OnTimerComplete;
            SunAnimator.speed = AnimationSpeed;
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
            yield return new WaitForSeconds(SecondsInADay);
            OnTimerComplete();
        }
        
        IEnumerator NightCoroutine()
        {
            yield return new WaitForSeconds(SecondsInADay);
            OnNightComplete();
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
            //SunAnimator.SetBool(Moving, false);
            StartCoroutine(NightCoroutine());
        }

        private void OnNightComplete()
        {
            SunAnimator.SetBool(Night, false);
            //SunAnimator.SetBool(Moving, false);
            StartCoroutine(DayCoroutine());
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