using System;
using UnityEngine;
using System.Collections;

namespace UI.TimeProgression
{
    public class Cloud : MonoBehaviour
    {
        private GameManager _g;
        [SerializeField] private Animator cloudAnimator;
        private static readonly int Night = Animator.StringToHash("night");

        public Animator CloudAnimator
        {
            get => cloudAnimator;
            private set => cloudAnimator = value;
        }

        private void Start()
        {
            _g = GameManager.current;
            _g.onRunStart += OnRunStart;
            //_g.onTimerComplete += OnTimerComplete;
            OnRunStart();
        }

        private void OnRunStart()
        {
            CloudAnimator.SetBool(Night, false);
            StartCoroutine(DayCoroutine());
        }

        IEnumerator DayCoroutine()
        {
            yield return new WaitForSeconds(180);
            OnTimerComplete();
            OnTimerComplete();
        }
        
        private void OnTimerComplete()
        {
            CloudAnimator.SetBool(Night, true);
        }

        private void OnDestroy()
        {
            _g.onRunStart -= OnRunStart;
            _g.onTimerComplete -= OnTimerComplete;
        }
    }
}