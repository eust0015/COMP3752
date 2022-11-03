using UnityEngine;
using System.Collections;

namespace UI.TimeProgression
{
    public class Cloud : MonoBehaviour
    {
        private GameManager _g;
        [SerializeField] private Animator cloudAnimator;
        [SerializeField] private int minutesInADay;
        private static readonly int Night = Animator.StringToHash("night");

        public Animator CloudAnimator
        {
            get => cloudAnimator;
            private set => cloudAnimator = value;
        }

        public int MinutesInADay
        {
            get => minutesInADay;
            set => minutesInADay = value;
        }
        
        private int SecondsInADay => minutesInADay * 60;

        private void Start()
        {
            var timer = FindObjectOfType<Timer>();
            if (timer != null)
                MinutesInADay = timer._timerStart / 60;
            _g = GameManager.current;
            _g.onRunStart += OnRunStart;
            _g.onTimerComplete += OnTimerComplete;
            OnRunStart();
        }

        private void OnRunStart()
        {
            CloudAnimator.SetBool(Night, false);
            //StartCoroutine(DayCoroutine());
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
        
        private void OnTimerComplete()
        {
            CloudAnimator.SetBool(Night, true);
            StartCoroutine(NightCoroutine());
        }

        private void OnNightComplete()
        {
            CloudAnimator.SetBool(Night, false);
            StartCoroutine(DayCoroutine());
        }
        
        private void OnDestroy()
        {
            _g.onRunStart -= OnRunStart;
            _g.onTimerComplete -= OnTimerComplete;
        }
    }
}